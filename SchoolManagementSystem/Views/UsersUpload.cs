using Microsoft.Win32;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Data.OleDb;
namespace SchoolManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for tryImport.xaml
    /// </summary>
    public partial class UsersUpload : Page
    {
        private SchoolMSEntities1 ty = new SchoolMSEntities1();
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        UsersUploadViewModel user1 = new UsersUploadViewModel();
        public UsersUpload()
        {
            InitializeComponent();
        }


        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
              user1.FillDataGrid();
        }

        private void btnimport_Click(object sender, RoutedEventArgs e)
        {
              user1.Import();
        }

          
       



    }
}
