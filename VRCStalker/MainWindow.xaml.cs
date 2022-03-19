using System;
using System.IO;
using System.Net;
using System.Windows;
using WebSocketSharp;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Toolkit.Uwp.Notifications;

using VRCStalker.VRCAPI;
using VRCStalker.VRCAPI.Auth;

using Newtonsoft.Json;


namespace VRCStalker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static MainWindow Instance;
        
        public UserWindow selectedUserWindow;

        public WebSocket vrcSocket;

        public AuthClient apiClient;

        public LoginWindow loginP;

        public string authToken = "NULL";
        public static string username = "NULL", password = "NULL";

        private bool locationChanges = false, statusChanges = false;

        public MainWindow()
        {
            InitializeComponent();
            InitMain();
            selectedUserWindow = new();
            selectedUserWindow.Closing += new((e, args) => 
            { 
                args.Cancel = true; 
                selectedUserWindow.Hide();
            });
            Closing += new((e, args) => { loginP.Close(); selectedUserWindow.Close(); });
        }

        public void InitMain()
        {
            Instance = this;

            if (FileManager.Instance == null) return;

            loginP = new();
            loginP.Show();
            Hide();
        }

        public void RefreshInfo()
        {
            vrcSocket = new($"wss://vrchat.com/?authToken={authToken}");
            vrcSocket.OnOpen += VrcSocket_OnOpen;
            vrcSocket.OnMessage += VrcSocket_OnMessage;
            vrcSocket.Connect();
            helloLabel.Content = $"Hello, {apiClient.currentUser.username}";

            locationToggle.IsChecked = FileManager.Instance.config.Location;
            statusToggle.IsChecked = FileManager.Instance.config.Status;

            foreach (Friend frnd in apiClient.friendsList)
            {
                string path = FileManager.Instance.CacheImage(string.IsNullOrEmpty(frnd.profilePicOverride) ? frnd.currentAvatarThumbnailImageUrl : frnd.profilePicOverride);

                Uri uPath = new(path);

                BitmapImage imgB = new(uPath);

                Image img = new();
                Button btn = new();

                btn.Background = Brushes.Transparent;
                btn.ToolTip = $"{frnd.displayName} || {frnd.statusDescription}";

                img.Height = 60;
                img.Width = 60;
                img.Source = imgB;

                btn.Height = 50;
                btn.Width = 60;
                btn.Content = img;

                btn.Click += new((e, args) => 
                {
                    selectedUserWindow.Show();
                    selectedUserWindow.selectedUser = apiClient.GetUserFromId(frnd.id);
                    selectedUserWindow.RefreshInfo(); 
                });

                Stack.Children.Add(btn);

            }
        }

        private void VrcSocket_OnMessage(object sender, MessageEventArgs e)
        {
            string jsonString = e.Data;
            SocketInfo dyn = JsonConvert.DeserializeObject<SocketInfo>(jsonString);
            if (dyn.content == null) return;
            

            switch (dyn.type.ToString().ToLower())
            {
                case "friend-location":

                    if (!locationChanges) break;
                    LocationContent locContent = JsonConvert.DeserializeObject<LocationContent>(dyn.content);
                    ToastContentBuilder tCBloc = new();
                    tCBloc.AddText("Location Change");
                    tCBloc.AddText($"{locContent.user.displayName} --> {(locContent.world.name == null ? "PRIVATE" : locContent.world.name)}");
                    tCBloc.Show();
                    break;

                case "friend-offline":

                    if (!statusChanges) break;

                    ToastContentBuilder tCBOff = new();
                    OfflineContent offContent = JsonConvert.DeserializeObject<OfflineContent>(dyn.content);
                    OtherUser user = apiClient.GetUserFromId(offContent.userId);
                    tCBOff.AddText("Friend Status");
                    tCBOff.AddText($"{user.displayName} --> OFFLINE");
                    tCBOff.Show();
                    break;
            }

            //type: notification, user-location, friend-location
        }

        private void VrcSocket_OnOpen(object sender, EventArgs e)
        {

        }

        private void locationToggle_Checked(object sender, RoutedEventArgs e)
        {
            locationChanges = true;
            FileManager.Instance.config.Location = true;
            FileManager.Instance.SaveConfig();
        }

        private void locationToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            locationChanges = false;
            FileManager.Instance.config.Location = false;
            FileManager.Instance.SaveConfig();
        }

        private void statusToggle_Checked(object sender, RoutedEventArgs e)
        {
            statusChanges = true;
            FileManager.Instance.config.Status = true;
            FileManager.Instance.SaveConfig();
        }

        private void statusToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            statusChanges = false;
            FileManager.Instance.config.Status = false;
            FileManager.Instance.SaveConfig();
        }
    }
}
