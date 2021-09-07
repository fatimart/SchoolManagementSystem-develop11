using SchoolManagementSystem.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for RegisterUsers.xaml
    /// </summary>
    /// 
    public partial class RegisterUsers : Page
    {
        

        UserViewModel _userViewModel = new UserViewModel();

        public RegisterUsers ()
        {
            InitializeComponent();

            // The DataContext serves as the starting point of Binding Paths
            //DataContext = _userViewModel;
        }


        private void Window_Loaded ( object sender, RoutedEventArgs e )
        {


            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Users. You can modify this code as needed.

            SchoolManagementSystem.SchoolMSDataSetTableAdapters.UsersTableAdapter schoolMSDataSetUsersTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.UsersTableAdapter();
            schoolMSDataSetUsersTableAdapter.Fill(schoolMSDataSet.Users);
            //  MessageBox.Show('"' + Application.Current.Resources["UserName"] + '"');
            //MARK: to not have them in the textboX
            //System.Windows.Data.CollectionViewSource usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usersViewSource")));
            //usersViewSource.View.MoveCurrentToFirst();

        }

        public void Load ()
        {
                        SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Users. You can modify this code as needed.
            SchoolMSDataSetTableAdapters.UsersTableAdapter schoolMSDataSetUsersTableAdapter = new SchoolMSDataSetTableAdapters.UsersTableAdapter();
            schoolMSDataSetUsersTableAdapter.Fill(schoolMSDataSet.Users);
        }

    

    }
}