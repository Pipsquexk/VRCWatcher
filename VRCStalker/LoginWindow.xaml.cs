using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Net.Http;
using System.Collections.Generic;

using Newtonsoft.Json;
using System.Windows.Controls;

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
