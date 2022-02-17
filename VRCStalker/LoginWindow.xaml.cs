using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Net.Http;
using System.Collections.Generic;

using Newtonsoft.Json;


namespace VRCStalker
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private bool loggedIn = false;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        public void Login()
        {
            string userpass = $"{usernameBox.Text}:{passwordBox.Password}";
            byte[] userpassBytes = System.Text.Encoding.UTF8.GetBytes(userpass);
            string basicAuth = Convert.ToBase64String(userpassBytes);

            CookieContainer cookies = new();
            HttpClientHandler handler = new()
            {
                CookieContainer = cookies
            };

            HttpClient httpsClient = new(handler);
            Uri reqUri = new("https://vrchat.com/api/1/auth/user?apiKey=JlE5Jldo5Jibnk5O5hTx6XVqsJu4WJ26");
            httpsClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36 OPR/83.0.4254.46");
            httpsClient.DefaultRequestHeaders.Add("Authorization", $"Basic {basicAuth}");
            HttpResponseMessage resp = httpsClient.GetAsync(reqUri).Result;

            try
            {
                IEnumerable<Cookie> cookiess = cookies.GetCookies(reqUri).Cast<Cookie>();
                string authCookie = cookiess.First().Value;

                MainWindow.Instance.authToken = authCookie;

                string username = (string)JsonConvert.DeserializeObject<dynamic>((string)resp.Content.ReadAsStringAsync().Result).username;

                MainWindow.Instance.username = username;
                MainWindow.Instance.RefreshInfo();
                MessageBox.Show("Successfully logged into VRChat!");
                MainWindow.Instance.Show();
                loggedIn = true;
                Close();
            }
            catch
            {
                MessageBox.Show("Uh oh, something went wrong.\nPlease check your credentials and try to log in again.");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!loggedIn) Application.Current.Shutdown();
        }
    }
}
