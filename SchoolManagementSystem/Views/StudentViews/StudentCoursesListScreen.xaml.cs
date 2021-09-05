﻿using SchoolManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace SchoolManagementSystem.Views.StudentViews
{
    /// <summary>
    /// Interaction logic for StudentCoursesListScreen.xaml
    /// </summary>
    public partial class StudentCoursesListScreen : Page
    {
        TimeTableViewModel table = new TimeTableViewModel();
        CourseRegistrationViewModel tablet = new CourseRegistrationViewModel();

        public StudentCoursesListScreen ()
        {
            InitializeComponent();
            DataContext = tablet;
        }

        private void Register_Click ( object sender, RoutedEventArgs e )
        {
            DateTime now = DateTime.Now;

            if (notEmpty())
            {

                table.InsertTimeTable(
                                    Convert.ToInt32(UserViewModel.userSession.UserID),
                                    Convert.ToInt32(txtcourseID.Text),
                                    roomNotxt.Text.ToString(),
                                    2021,
                                    "techer name test",
                                    coursenametxt.Text.ToString(),
                                    timetxt.Text.ToString(),
                                    course_combo_box.Text.ToString(),
                                    Convert.ToInt32(sectionnotxt.Text.ToString()),
                                    Convert.ToDateTime(examDatetxt.Text.ToString())
                                   );
                //FillDataGrid();

            }
            else
            {
                MessageBox.Show("Please Select a course!!");
            }
        }

        private void cancel_Click ( object sender, RoutedEventArgs e )
        {
            course_combo_box.SelectedItem = "";
            course_combo_box.Text = "";
            coursenametxt.Text = "";
            examDatetxt.Text = "";
            sectionnotxt.Text = "";
            timetxt.Text = "";
            roomNotxt.Text = "";
            teachernametxt.Text = "";
            txtcourseID.Text = "";
        }

        private void btnRemove_Click ( object sender, RoutedEventArgs e )
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            String CourseCode = dataRowView[0].ToString();
            String CourseName = dataRowView[1].ToString();

            if (MessageBox.Show("Are You sure you want to delete : " + CourseCode + "\r\n" + CourseName + "?", "Course", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    table.DeleteTimeTable(UserViewModel.userSession.UserID, CourseCode);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //FillDataGrid();
                }
            }
        }

        public bool notEmpty ()
        {
            if (course_combo_box.Text == "")
            { return false; }
            else { return true; }
        }

        private void course_combo_box_DropDownClosed ( object sender, EventArgs e )
        {
            //fillCourseDetails();
            course_details_groupbox.Visibility = Visibility.Visible;


        }
    }
}
