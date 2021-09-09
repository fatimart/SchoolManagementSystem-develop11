using SchoolManagementSystem.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for StudentGradeScreen.xaml
    /// </summary>
    public partial class StudentGradeScreen : Page
    {
     
        StudentGradeViewModel SGrades = new StudentGradeViewModel();
        public StudentGradeScreen()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Student Grade . You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.StudentGradeTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.StudentGradeTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.StudentGrade);
        }
        public void Load()
        {
            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Student Grade . You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.StudentGradeTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.StudentGradeTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.StudentGrade);

        }
        /**
        private void Button_Click1(object sender, RoutedEventArgs e)
        {//add
            bool done = (bool)doneCheckBox.IsChecked;
            SGrades.AddStudentGrade(Convert.ToInt32(courseIDTextBox.Text), Convert.ToInt32(studentIDTextBox.Text), Convert.ToInt32(scoreTextBox.Text), Convert.ToInt32(attendanceTextBox.Text), done, Convert.ToInt32(yearIDTextBox.Text));
            Load();
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {//update

            if (SGrades.CheckSGID(Convert.ToInt32(iDTextBox.Text)))
            {
                bool done = (bool)doneCheckBox.IsChecked;

                SGrades.UpdateStudentGrade(Convert.ToInt32(iDTextBox.Text),
                    Convert.ToInt32(courseIDTextBox.Text),
                    Convert.ToInt32(studentIDTextBox.Text), 
                    Convert.ToInt32(scoreTextBox.Text), 
                    Convert.ToInt32(attendanceTextBox.Text), 
                    done, 
                    Convert.ToInt32(yearIDTextBox.Text));
                Load();

            }

            else
            {
                MessageBox.Show("ID not existed");
            }


        }
    **/
        private void Button_Click4(object sender, RoutedEventArgs e)
        {//delete

            if (SGrades.CheckSGID(Convert.ToInt32(iDTextBox.Text)))
            {

                SGrades.deleteStudentGrade(Convert.ToInt32(iDTextBox.Text));
                Load();

            }

            else
            {
                MessageBox.Show("ID not existed");
            }
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            iDTextBox.Text = courseIDTextBox.Text = studentIDTextBox.Text = scoreTextBox.Text = attendanceTextBox.Text = yearIDTextBox.Text = "";
            if ((bool)doneCheckBox.IsChecked == true)
            { doneCheckBox.IsChecked = false;}

        }
    }
}
