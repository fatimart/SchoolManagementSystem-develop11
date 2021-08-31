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
            Fillcombobox();
        }

        private void Register_Click ( object sender, RoutedEventArgs e )
        {
            DateTime now = DateTime.Now;

            /**table.AddTimeTable(
                                Convert.ToInt32(UserViewModel.userSession.UserID),
                                CourseID.Text.Trim(),
                                SectionID.Text.Trim(),
                                emailTextBox.Text.Trim(),
                                RoomID.Text.Trim(),
                                now,
                                TeacherName.Text.Trim()
                               );**/
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

        /**
        public void fillCourseBox ()
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

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        **/

        public void Fillcombobox ()
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("select CourseCode From Course", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    course_combo_box.Items.Add(Sdr.GetString(i));

                }
            }
            course_combo_box.DisplayMemberPath = "contactname";

            Sdr.Close();
            con.Close();

        }
    }
}
