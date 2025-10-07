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

namespace Sensors
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public string? UserName { get; private set; }
        public string? Password { get; private set; }

        public UserLogin()
        {
            InitializeComponent();
            UserNameBox.Focus();
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            // Basic required-field check; extend as needed
            if (string.IsNullOrWhiteSpace(UserNameBox.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show(this, "Enter username and password.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UserName = UserNameBox.Text.Trim();
            Password = PasswordBox.Password;

            Dashboard dash = new Dashboard();
            dash.Show();
            Close();
        }
    }
}

