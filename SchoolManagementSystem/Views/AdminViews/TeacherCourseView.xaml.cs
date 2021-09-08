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

namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for TeacherCourseView.xaml
    /// </summary>
    public partial class TeacherCourseView : Page
    {
        TeacherCourseViewModel TeacherCourse = new TeacherCourseViewModel();
        public TeacherCourseView ()
        {
            InitializeComponent();
            TeacherCourse.FillCourseBox();
            TeacherCourse.FillTeacherBox();
            TeacherCourse.FillCourseDetails();
        }

        private void Course_combo_box_DropDownClosed ( object sender, EventArgs e )
        {


        }
        private void Teacher_combo_box_DropDownClosed ( object sender, EventArgs e )
        {



        }

        private void course_combo_box_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {

        }

        private void teacher_combo_box_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {

        }

        private void Button_Click ( object sender, RoutedEventArgs e )
        {//add
            TeacherCourse.AddTeacherCourse(teacher_combo_box.Text.ToString(), course_combo_box.Text.ToString());
            TeacherCourse.FillCourseDetails();
        }

        private void Button_Click_1 ( object sender, RoutedEventArgs e )
        {//update
            TeacherCourse.UpdateTeacherCourse(Convert.ToInt32(TIDTextBox.Text), teacher_combo_box.Text.ToString(), course_combo_box.Text.ToString());
            TeacherCourse.FillCourseDetails();
        }

        private void Button_Click_2 ( object sender, RoutedEventArgs e )
        {//delete
            TeacherCourse.DeleteTeacherCourse(Convert.ToInt32(TIDTextBox.Text));
            TeacherCourse.FillCourseDetails();
        }

        private void find_Click ( object sender, RoutedEventArgs e )
        {
            // TeacherCourse.FindTeacherCourse(Convert.ToInt32(TIDTextBox.Text));
        }
    }
}
