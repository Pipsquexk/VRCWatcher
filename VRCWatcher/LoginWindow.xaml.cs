using System;
using System.Net;
using System.Linq;
using System.Windows;

using Newtonsoft.Json;


namespace VRCWatcher
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
            rememberMe.IsChecked = FileManager.Instance.config.Remember;
            if (!FileManager.Instance.config.Remember) return;
            usernameBox.Text = FileManager.Instance.config.Username;
            passwordBox.Password = FileManager.Instance.config.Password;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        public void Login()
        {
            try
            {
                MainWindow.Instance.apiClient = new(usernameBox.Text, passwordBox.Password);

                if (MainWindow.Instance.apiClient.currentUser == null)
                {
                    throw new Exception("Bad login attempt");
                }

                MainWindow.Instance.RefreshInfo();
                MessageBox.Show("Successfully logged into VRChat!");
                MainWindow.Instance.Show();

                FileManager.Instance.config.Remember = rememberMe.IsChecked.Value;

                if(rememberMe.IsChecked.Value)
                {
                    FileManager.Instance.config.Username = usernameBox.Text;
                    FileManager.Instance.config.Password = passwordBox.Password;
                }

                FileManager.Instance.SaveConfig();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Uh oh, something went wrong.\nPlease check your credentials and try to log in again.\n {ex.ToString()}");
            }
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!loggedIn) Application.Current.Shutdown();
        }

    }
}
