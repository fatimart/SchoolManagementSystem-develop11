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

namespace SchoolManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for TeacherCourseView.xaml
    /// </summary>
    public partial class TeacherCourseView : Page
    {
        TeacherCourseViewModel TeacherCourse = new TeacherCourseViewModel();
        public TeacherCourseView()
        {
            InitializeComponent();
            FillCourseBox();
            FillTeacherBox();
            FillCourseDetails();
        }

        public void FillCourseBox()
        {
            try
            {
                
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT CourseCode from Course;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the courses names

                course_combo_box.ItemsSource = dt.DefaultView;
                course_combo_box.SelectedIndex = -1;
                course_combo_box.DisplayMemberPath = "CourseCode";
                course_combo_box.SelectedValuePath = "CourseCode";


                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void FillTeacherBox()
        {
            try
            {
               // teacher_combo_box.Items.Clear();
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("select Name from Users where Type='" + "teacher" + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the teachers names

                teacher_combo_box.ItemsSource = dt.DefaultView;
                teacher_combo_box.SelectedIndex = -1;
                teacher_combo_box.DisplayMemberPath = "Name";
                teacher_combo_box.SelectedValuePath = "Name";


                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void FillCourseDetails()
        {
            try
            {

                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("select * from TeacherCourses ", connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dtGrid.ItemsSource = dt.DefaultView;
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }




        private void Course_combo_box_DropDownClosed(object sender, EventArgs e)
        {
            

        }
        private void Teacher_combo_box_DropDownClosed(object sender, EventArgs e)
        {
           


        }

        private void course_combo_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void teacher_combo_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {//add
            TeacherCourse.AddTeacherCourse(teacher_combo_box.Text.ToString(),course_combo_box.Text.ToString());
            FillCourseDetails();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {//update
            TeacherCourse.UpdateTeacherCourse(Convert.ToInt32(TIDTextBox.Text), teacher_combo_box.Text.ToString(), course_combo_box.Text.ToString());
            FillCourseDetails();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {//delete
            TeacherCourse.DeleteTeacherCourse(Convert.ToInt32(TIDTextBox.Text));
            FillCourseDetails();
        }

        private void find_Click(object sender, RoutedEventArgs e)
        {
           // TeacherCourse.FindTeacherCourse(Convert.ToInt32(TIDTextBox.Text));
        }
    }
}
