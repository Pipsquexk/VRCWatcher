using System;
using System.Windows;
using WebSocketSharp;
using Newtonsoft.Json;
using Microsoft.Toolkit.Uwp.Notifications;

using VRCStalker.VRCAPI;
using System.Windows.Controls;
using System.Net;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VRCStalker.VRCAPI.Auth;

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

        public string authToken = "NULL";

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
        }

        public void InitMain()
        {
            Instance = this;
            Cache.Init();
            LoginWindow loginP = new();
            loginP.Show();
            Hide();
        }

        public async void RefreshInfo()
        {
            vrcSocket = new($"wss://vrchat.com/?authToken={authToken}");
            vrcSocket.OnOpen += VrcSocket_OnOpen;
            vrcSocket.OnMessage += VrcSocket_OnMessage;
            vrcSocket.Connect();
            helloLabel.Content = $"Hello, {apiClient.currentUser.username}";

            foreach (Friend frnd in apiClient.friendsList)
            {
                string path = Utils.CacheImage(string.IsNullOrEmpty(frnd.profilePicOverride) ? frnd.currentAvatarThumbnailImageUrl : frnd.profilePicOverride);

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
        }

        private void locationToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            locationChanges = false;
        }

        private void statusToggle_Checked(object sender, RoutedEventArgs e)
        {
            statusChanges = true;
        }

        private void statusToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            statusChanges = false;
        }
    }
}
