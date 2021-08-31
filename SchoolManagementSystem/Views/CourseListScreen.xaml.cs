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
using System.Data.SqlClient;
using SchoolManagementSystem;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;

namespace SchoolManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for CourseListScreen.xaml
    /// </summary>
    public partial class CourseListScreen : Page
    {
        CourseViewModel course = new CourseViewModel();
        private readonly CourseViewModel courseViewModel ;
        public CourseListScreen()
        {
            InitializeComponent();
               courseViewModel = new CourseViewModel();

            // The DataContext serves as the starting point of Binding Paths
            DataContext = courseViewModel;


        }

        
        public void Load()
        {
            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Course. You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.CourseTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.CourseTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.Course);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
               System.Windows.Data.CollectionViewSource courseViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("courseViewSource")));
              courseViewSource.View.MoveCurrentToFirst();

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            //add button 
            course.AddCourse(courseNameTextBox.Text.Trim(), courseCodeTextBox.Text.Trim(), descriptionTextBox.Text.Trim(), Convert.ToDateTime(examDateDatePicker.Text));
            Load();
        }
        public void Clear()
        {

        }
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            course.Clear();
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            if (course.CheckCourseID(Convert.ToInt32(courseIDTextBox.Text)))
            {

                course.UpdateCourse(Convert.ToInt32(courseIDTextBox.Text.Trim()), courseNameTextBox.Text.Trim(), courseCodeTextBox.Text.Trim(), descriptionTextBox.Text.Trim(), Convert.ToDateTime(examDateDatePicker.Text));
                Load();

            }

            else
            {
                MessageBox.Show("ID not existed");
            }


        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            //delete button 
            if (course.CheckCourseID(Convert.ToInt32(courseIDTextBox.Text)))
            {

                course.DeleteCourse(Convert.ToInt32(courseIDTextBox.Text));
                Load();

            }


            else
            {
                MessageBox.Show("ID not existed");
            }

        }




    }
}
