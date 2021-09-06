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

namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for SectionScreen.xaml
    /// </summary>
    public partial class SectionScreen : Page
    {

        public SectionScreen()
        {
            InitializeComponent();

            fillCourseBox();
            fillSectioneBox();
            FillDataGrid();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {

        }

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

                course_code_combobox.ItemsSource = dt.DefaultView;
                course_code_combobox.SelectedIndex = -1;
                course_code_combobox.DisplayMemberPath = "CourseCode";
                course_code_combobox.SelectedValuePath = "CourseCode";


                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void fillSectioneBox ()
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT RoomNum from Room;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the courses names

                roomNo_combobox.ItemsSource = dt.DefaultView;
                roomNo_combobox.SelectedIndex = -1;
                roomNo_combobox.DisplayMemberPath = "RoomNum";
                roomNo_combobox.SelectedValuePath = "RoomNum";


                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void FillDataGrid ()

        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                SqlCommand command = new SqlCommand(
                   "select Section.SectionID, Course.CourseCode, Section.SectionNum, Room.RoomNum from Section,Course,Room where Section.CourseID=Course.CourseID AND Section.RoomID=Room.RoomNum", connection);
                connection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(command);

                DataTable dt = new DataTable("Section");

                sda.Fill(dt);
                sectionDataGrid.ItemsSource = dt.DefaultView;

                //usersDataGrid.ItemsSource = _userViewModel.AllSections;

                sectionDataGrid.Items.Refresh();

            }

        }


    }
}
