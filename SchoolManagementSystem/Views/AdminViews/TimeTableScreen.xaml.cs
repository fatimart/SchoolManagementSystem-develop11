using SchoolManagementSystem.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for TimeTableScreen.xaml
    /// </summary>
    public partial class TimeTableScreen : Page
    {
        TimeTableViewModel TimeTable = new TimeTableViewModel();
        public TimeTableScreen()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table TimeTable. You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.TimeTableTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.TimeTableTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.TimeTable);
        }
        public void Load()
        {
            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table TimeTable. You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.TimeTableTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.TimeTableTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.TimeTable);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            //add
            //TimeTable.AddTimeTable(Convert.ToInt32(courseIDTextBox.Text), Convert.ToInt32(sectionIDTextBox.Text), Convert.ToInt32(roomIDTextBox.Text), Convert.ToInt32(yearIDTextBox.Text), teacherNameTextBox.Text.Trim());
            Load();
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            /**
            if (TimeTable.CheckTimeTableID(Convert.ToInt32(timeTableIDTextBox.Text)))
            {
                TimeTable.UpdateTimeTable(Convert.ToInt32(courseIDTextBox.Text), Convert.ToInt32(sectionIDTextBox.Text), Convert.ToInt32(roomIDTextBox.Text), Convert.ToInt32(yearIDTextBox.Text), Convert.ToInt32(timeTableIDTextBox.Text), teacherNameTextBox.Text.Trim());
                Load();

            }

            else
            {
                MessageBox.Show("ID not existed");
            }
            **/
          
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            ////delete
            //if (TimeTable.CheckTimeTableID(Convert.ToInt32(timeTableIDTextBox.Text)))
            //{
            //    TimeTable.DeleteTimeTable(Convert.ToInt32(timeTableIDTextBox.Text));
            //    Load();

            //}

            //else
            //{
            //    MessageBox.Show("ID not existed");
            //}
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            //clear 
        }
    }
}
