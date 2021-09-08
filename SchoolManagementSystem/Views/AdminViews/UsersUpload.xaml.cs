using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for UsersUpload.xaml
    /// </summary>
    public partial class UsersUpload : Page
    {
        private SchoolMSEntities1 ty = new SchoolMSEntities1();
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        UsersUploadViewModel user1 = new UsersUploadViewModel();
        public UsersUpload ()
        {
            InitializeComponent();
        }


        private void btnOpen_Click ( object sender, RoutedEventArgs e )
        {
            user1.FillDataGrid();
        }

        private void btnimport_Click ( object sender, RoutedEventArgs e )
        {
            user1.Import();
        }
    }
}
