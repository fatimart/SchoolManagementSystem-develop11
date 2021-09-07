using SchoolManagementSystem.ViewModels;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views.StudentViews
{
    /// <summary>
    /// Interaction logic for CourseDetailScreen.xaml
    /// </summary>
    public partial class CourseDetailScreen : Page
    {
        AnnoucnmentViewModel viewModel = new AnnoucnmentViewModel();
        CourseViewModel courseviewModel = new CourseViewModel();

        public static int courseID;

        public CourseDetailScreen ()
        {
            InitializeComponent();
            fillCourseBox2();
            fillCourseDetails();
            //DataContext = viewModel;

        }

        public void fillCourseDetails ()
        {
            foreach (var data in viewModel.AllAnnounc)
            {
                if (data.CourseID == courseID)
                {
                    announ_datagrid.ItemsSource = viewModel.AllAnnounc;
                }
            }
        }

        public void fillCourseBox2 ()
        {
            try
            {
                course_combo_box.Items.Clear();

                foreach (var data in courseviewModel.AllCourses)
                {
                    course_combo_box.SelectedIndex = -1;
                    course_combo_box.ItemsSource = data.CourseCode;
                    course_combo_box.DisplayMemberPath = data.CourseCode.ToString();
                    course_combo_box.SelectedValuePath = "CourseCode";
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void fillCourseBox ()
        {
            try
            {
                course_combo_box.Items.Clear();

                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT CourseCode, CourseID from Course;", con);
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

        public void getcourseIDD ()
        {

            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT CourseCode,CourseID from Course where CourseCode='" + course_combo_box.Text.ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the courses names

                courseID = Convert.ToInt32(dt.Rows[0]["CourseID"].ToString());

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void course_combo_box_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {

        }

        private void course_combo_box_DropDownClosed ( object sender, EventArgs e )
        {
            if (course_combo_box.Text != "")
            {
                getcourseIDD();
            }
        }
    }
}
