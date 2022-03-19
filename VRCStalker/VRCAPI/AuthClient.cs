using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Windows;
using System.Net.Http;
using System.Collections.Generic;

using Newtonsoft.Json;

using VRCStalker.VRCAPI.Auth;


namespace VRCStalker.VRCAPI
{
    public class AuthClient
    {
        private string authCookie = "NULL";

        public User currentUser = null;

        public List<Friend> friendsList;

        private HttpClient client;

        public AuthClient(string username, string password) => Init(username, password);

        public void Init(string username, string password)
        {
            try
            {
                string userpass = $"{username}:{password}";
                byte[] userpassBytes = Encoding.UTF8.GetBytes(userpass);
                string basicAuth = Convert.ToBase64String(userpassBytes);

                CookieContainer cookies = new();
                HttpClientHandler handler = new()
                {
                    CookieContainer = cookies
                };


                client = new(handler);
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36 OPR/83.0.4254.46");
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {basicAuth}");
                Uri reqUri = new("https://vrchat.com/api/1/auth/user?apiKey=JlE5Jldo5Jibnk5O5hTx6XVqsJu4WJ26");
                HttpResponseMessage resp = client.GetAsync(reqUri).Result;


                IEnumerable<Cookie> cookiess = cookies.GetCookies(reqUri).Cast<Cookie>();
                authCookie = cookiess.First().Value;

                client.DefaultRequestHeaders.Remove("Authorization");

                string pageStr = resp.Content.ReadAsStringAsync().Result;

                User us = JsonConvert.DeserializeObject<User>(pageStr);

                currentUser = us;

                client.DefaultRequestHeaders.Add("cookie", $"auth={authCookie}");
                friendsList = new();
                FetchFriends();
            }
            catch { }
        }

        public void FetchFriends()
        {
            try
            {
                Uri reqUri = new("https://vrchat.com/api/1/auth/user/friends?offline=false&n=50&offset=0&apiKey=JlE5Jldo5Jibnk5O5hTx6XVqsJu4WJ26");
                HttpResponseMessage resp = client.GetAsync(reqUri).Result;

                string pageStr = resp.Content.ReadAsStringAsync().Result;

                List<Friend> tempList = JsonConvert.DeserializeObject<List<Friend>>(pageStr);

                friendsList.AddRange(tempList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<Friend> GetFriends(bool offline = false, int n = 50, int offset = 0)
        {
            Uri reqUri = new("https://vrchat.com/api/1/auth/user/friends?offline=false&n=50&offset=0&apiKey=JlE5Jldo5Jibnk5O5hTx6XVqsJu4WJ26");
            HttpResponseMessage resp = client.GetAsync(reqUri).Result;

            string pageStr = resp.Content.ReadAsStringAsync().Result;

            List<Friend> friends = JsonConvert.DeserializeObject<List<Friend>>(pageStr); ;

            return friends;
        }

        public OtherUser GetUserFromId(string id)
        {
            Uri reqUri = new($"https://vrchat.com/api/1/users/{id}");
            HttpResponseMessage resp = client.GetAsync(reqUri).Result;

            string pageStr = resp.Content.ReadAsStringAsync().Result;

            OtherUser retUser = JsonConvert.DeserializeObject<OtherUser>(pageStr);

            return retUser;
        }

    }
}
