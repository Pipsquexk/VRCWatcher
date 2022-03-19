using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;

using VRCStalker.VRCAPI.Auth;


namespace VRCStalker
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public OtherUser selectedUser;

        public UserWindow()
        {
            InitializeComponent();
        }

        public void RefreshInfo()
        {
            statusLabel.Text = $"Status - {selectedUser.status}";

            userLabel.Text = $"Selected User: {selectedUser.displayName}";
            displayNameLabel.Text = selectedUser.displayName;
            usernameLabel.Text = selectedUser.username;
            idLabel.Text = selectedUser.id;

            bioTextBox.Text = $"Bio:\n{selectedUser.bio}";
        }
    }
}
