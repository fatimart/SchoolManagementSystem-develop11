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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for YearScreen.xaml
    /// </summary>
    public partial class YearScreen : Page
    {
        YearViewModel year = new YearViewModel();
        public YearScreen()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Year. You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.YearTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.YearTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.Year);
        }

        public void Load()
        {
            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Year. You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.YearTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.YearTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.Year);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {//add
            year.AddYear(yearNumTextBox.Text.Trim());
            Load();

        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {//update
            if (year.CheckYearID(Convert.ToInt32(yearIDTextBox.Text)))
            {

                year.UpdateYear(Convert.ToInt32(yearIDTextBox.Text), yearNumTextBox.Text.Trim());
                Load();

            }

            else
            {
                MessageBox.Show("ID not existed");
            }

        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {//delete 
            if (year.CheckYearID(Convert.ToInt32(yearIDTextBox.Text)))
            {

                year.DeleteYear(Convert.ToInt32(yearIDTextBox.Text));
                Load();

            }

            else
            {
                MessageBox.Show("ID not existed");
            }

        }
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            yearIDTextBox.Text = yearNumTextBox.Text = "";
        }
    }
}

  

