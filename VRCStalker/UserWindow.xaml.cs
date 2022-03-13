using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
            statusLabel.Content = $"Status - {selectedUser.status}";

            userLabel.Content = $"Selected User: {selectedUser.displayName}";
            displayNameLabel.Content = selectedUser.displayName;
            usernameLabel.Content = selectedUser.username;
            idLabel.Content = selectedUser.id;

            bioTextBox.Text = $"Bio:\n{selectedUser.bio}";
        }
    }
}
