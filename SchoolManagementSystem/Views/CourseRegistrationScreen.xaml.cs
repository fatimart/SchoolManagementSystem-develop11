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
using System.Windows.Shapes;

namespace SchoolManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for CourseRegistrationScreen.xaml
    /// </summary>
    public partial class CourseRegistrationScreen : Page
    {
        CourseViewModel course = new CourseViewModel();
        TimeTableViewModel table = new TimeTableViewModel();

        public CourseRegistrationScreen ()
        {
            InitializeComponent();
            FillDataGrid();
            fillCourseBox();

        }

        private void Register_Click ( object sender, RoutedEventArgs e )
        {
            DateTime now = DateTime.Now;

            table.AddTimeTable(
                0,
                                10,
                                10,
                                10,
                                100,
                                "aa"
                               );
        }

        private void DeleteCourse_Click ( object sender, RoutedEventArgs e )
        {
            //table.DeleteTimeTable(Convert.ToInt32(userIDTextBox.Text.Trim()));
        }

        private void FillDataGrid ()

        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                SqlCommand command = new SqlCommand(
                     "select * from TimeTable where UserID='" + UserViewModel.userSession.UserID + "'", connection);
                connection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(command);

                DataTable dt = new DataTable("TimeTable");

                sda.Fill(dt);

                tableDataGrid.ItemsSource = dt.DefaultView;
                tableDataGrid.Items.Refresh();

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

        private void course_combo_box_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
          
        }


        public void fillCourseDetails ()
        {
            try
            {

                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("select * from Course where CourseCode='" + course_combo_box.Text.ToString() + "'", connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {
                            coursenametxt.Text = dt.Rows[0]["CourseName"].ToString();
                            examDatetxt.Text = dt.Rows[0]["ExamDate"].ToString(); 
                    }
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

       

        private void cancel_Click ( object sender, RoutedEventArgs e )
        {
            course_combo_box.SelectedItem = "";
            course_combo_box.Text = "";
            course_details_groupbox.Visibility = Visibility.Hidden;



        }

        private void course_combo_box_DropDownClosed ( object sender, EventArgs e )
        {
            fillCourseDetails();
            course_details_groupbox.Visibility = Visibility.Visible;


        }
    }
}
