using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;
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

namespace SchoolManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for ProfileScreen.xaml
    /// </summary>
    public partial class ProfileScreen : Page
    {
        EditProfileViewModel editView = new EditProfileViewModel();

        public ProfileScreen ()
        {
            InitializeComponent();
            DataContext = editView.user;
        }


        private void Window_Loaded ( object sender, RoutedEventArgs e )
        {
            string type = UserViewModel.userSession.Type.ToString();

        }

        private void update_profile_Click ( object sender, RoutedEventArgs e )
        {
            string pass = "";
            if (txtnewPass.Text.Trim() == "")
            {
                pass = passwordTextBox.Text.Trim();
            }
            else
            {
                //i have set a new password
                pass = txtnewPass.Text.Trim();
            }

            editView.UpdateUser(
                                 Convert.ToInt32(userIDTextBox.Text.Trim()),
                                      nameTextBox.Text.Trim(),
                                      emailTextBox.Text.Trim(),
                                      Convert.ToDecimal(cPRTextBox.Text),
                                      addressTextBox.Text.Trim(),
                                      Convert.ToDateTime(dOBDatePicker.Text),
                                      pass,
                                      contactNoTextBox.Text.Trim()
                               );
        }

    }
}
