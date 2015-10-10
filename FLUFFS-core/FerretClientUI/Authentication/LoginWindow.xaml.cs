using FirstFloor.ModernUI.Windows.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FerretClientUI.Authentication
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : ModernWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ModernButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (AuthenticationManager.Authenticate
                (txtUserName.Text, pwbPasswordBox.Password) == false)
            {
                ModernDialog.ShowMessage
                    ("Invalid credentials", "ERROR", MessageBoxButton.OK);
            }

            //if got this far then authenticated OK.
            if (AuthenticationManager.CurrentUser.NewPasswordDue)
            {
                Window window = new ChangePasswordWindow();
                this.Hide();
                window.ShowDialog();
                this.Close();
            }
        }
    }
}
