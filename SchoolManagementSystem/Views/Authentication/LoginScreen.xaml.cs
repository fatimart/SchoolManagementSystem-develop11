
using System.Windows;
using SchoolManagementSystem.Views;
using SchoolManagementSystem.ViewModels;

namespace SchoolManagementSystem
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        UserViewModel userViewModel = new UserViewModel();
        

        public LoginScreen ()
        {
            InitializeComponent();
        }


        private void btnSubmit_Click ( object sender, RoutedEventArgs e )
        {
            if(userViewModel.login(txtUsername.Text.Trim(), txtPassword.Password.ToString()))
            {

               // StudentsListScreen dashboard = new StudentsListScreen();
                //dashboard.Show();
              
                layout dashboard1 = new layout();
                dashboard1.Show();
                this.Close();
            }
        }

        //MARK: How to use for looop for the viewmodel
        /**
         * User user1 = userViewModel.login(txtUsername.Text.Trim(), txtPassword.Password.ToString());
         * UserViewModel.session = new User();
         * 
         * 
         * List<User> users = userViewModel.getUsers(txtUsername.Text, "");

        foreach (var item in users)
        {
            MessageBox.Show(item.UserName);
            txtUsername.Text = item.UserName;
        }**/

    }
}