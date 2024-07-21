using AirConditionerShop.BLL.Services;
using AirConditionerShop.DAL.Entities;
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

namespace AirConditionerShop_TranNgocKinhLuan
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private StaffMemberService _service = new();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            StaffMember staff = _service.authorize(EmailTextBox.Text, PasswordTextBox.Text);
            if (staff == null)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow mainWindow = new MainWindow(staff);
            mainWindow.Show();
            this.Close();
        }
    }
}

