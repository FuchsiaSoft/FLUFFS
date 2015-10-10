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
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : ModernWindow
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ModernButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (pwbPasswordBox1.Password != pwbPasswordBox2.Password)
            {
                ModernDialog.ShowMessage("Passwords do not match", "ERROR", MessageBoxButton.OK);
                return;
            }

            AuthenticationManager.CurrentUser.ChangePassword(pwbPasswordBox1.Password);

            Window window = new MainWindow();

            window.Show();

            this.Close();
        }
    }
}
