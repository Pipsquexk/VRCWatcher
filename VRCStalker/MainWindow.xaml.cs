using System;
using System.Windows;
using WebSocketSharp;
using Newtonsoft.Json;
using Microsoft.Toolkit.Uwp.Notifications;

namespace VRCStalker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static MainWindow Instance;

        public WebSocket vrcSocket;

        public string username = "NULL";

        public string authToken = "NULL";

        private bool locationChanges = true;

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            LoginWindow loginP = new();
            loginP.Show();
            Hide();
        }

        public void RefreshInfo()
        {
            vrcSocket = new($"wss://vrchat.com/?authToken={authToken}");
            vrcSocket.OnOpen += VrcSocket_OnOpen;
            vrcSocket.OnMessage += VrcSocket_OnMessage;
            vrcSocket.Connect();
            helloLabel.Content = $"Hello, {username}";
        }

        private void VrcSocket_OnMessage(object sender, MessageEventArgs e)
        {
            string jsonString = e.Data;
            SocketInfo dyn = JsonConvert.DeserializeObject<SocketInfo>(jsonString);
            SocketContent content = JsonConvert.DeserializeObject<SocketContent>(dyn.content);

            switch (dyn.type.ToString().ToLower())
            {
                case "friend-location":

                    if (!locationChanges) break;

                    ToastContentBuilder tCB = new();
                    tCB.AddText("Location Change");
                    tCB.AddText($"{content.user.displayName} --> {(content.world.name == null ? "PRIVATE" : content.world.name)}");
                    tCB.Show();
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
    }
}
