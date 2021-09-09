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

        SqlDataAdapter dataAdapter;
        SqlDataAdapter sda1;
        DataTable dt1;

        public static int courseID;

        public CourseDetailScreen ()
        {
            InitializeComponent();
            DataFill();
            CourseComboBox();
            comboBoxValue();
        }


        public void DataFill ()
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
                try
                {
                    SqlCommand command = new SqlCommand(
                         "select Course.CourseID,Course.CourseName,Course.CourseCode,TimeTable.SectionNo,TimeTable.Time,TimeTable.RoomNo from Course,TimeTable,Users where Users.UserID='" + UserViewModel.userSession.UserID + "'AND Users.UserID=TimeTable.UserID And Course.CourseID=TimeTable.CourseID", connection);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dGrid.ItemsSource = dt.DefaultView;
                    connection.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
        }
        public void CourseDataFill ()
        {

            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
                try
                {
                    connection.Open();
                    var CID = course_combo_box.Text.Split('-');
                    string cid = CID[0];
                    string cCode = CID[1];
                    string CSection = CID[3];
                    SqlCommand command = new SqlCommand(
            "select StudentGrade.ID,StudentGrade.StudentID,Users.Name,StudentGrade.Score,StudentGrade.Attendance,StudentGrade.Done from StudentGrade, TimeTable, Users, Section where Users.UserID = TimeTable.UserID AND TimeTable.UserID = StudentGrade.StudentID AND Users.UserID = StudentGrade.StudentID AND TimeTable.SectionID = StudentGrade.SectionID AND TimeTable.SectionID = Section.SectionID AND Section.SectionID = StudentGrade.SectionID AND TimeTable.CourseID = StudentGrade.CourseID AND TimeTable.CourseID = Section.CourseID AND Section.CourseID = StudentGrade.CourseID AND StudentGrade.CourseID='" + cid + "'", connection);
                    dataAdapter = new SqlDataAdapter(command);
                    //   DataTable dt = new DataTable();


                    dataAdapter.Fill(dt1);
                    dGrid.ItemsSource = dt1.DefaultView;

                    //   connection.Close();
                    CourseTxt.Content = cCode;
                    SectionTxt.Text = CSection;

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
        }


        public string comboBoxValue ()
        {
            string S = course_combo_box.Text;
            return S;
        }
        public void CourseComboBox ()
        {
            try
            {

                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("select Concat(Course.CourseID,'-',TimeTable.CourseCode,'-Section-',TimeTable.SectionNo) AS CodeSection from Course, TimeTable, Users where Users.UserID = '" + UserViewModel.userSession.UserID + "'AND Users.UserID = TimeTable.UserID AND Course.CourseID = TimeTable.CourseID", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the courses names

                course_combo_box.ItemsSource = dt.DefaultView;
                course_combo_box.SelectedIndex = -1;
                course_combo_box.DisplayMemberPath = "CodeSection";
                course_combo_box.SelectedValuePath = "CourseID";

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void Course_combo_box_DropDownClosed ( object sender, EventArgs e )
        {
            if (CheckComboBox())
            {
                CourseDataFill1();
            }
            else { return; }

        }

        private void Reset_Click ( object sender, RoutedEventArgs e )
        {
            course_combo_box.SelectedItem = "";
            course_combo_box.Text = "";

            DataFill();
           


        }

        public bool CheckComboBox ()
        {
            string S = course_combo_box.Text;
            if (S.Length <= 0)
            {
                return false;
            }

            else
            {
                return true;
            }

        }

        public void CourseDataFill1 ()
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            var CID = course_combo_box.Text.Split('-');
            string cid = CID[0];

            SqlConnection con = new SqlConnection(strcon);
            sda1 = new SqlDataAdapter("select StudentGrade.ID,StudentGrade.StudentID,Users.Name,StudentGrade.Score,StudentGrade.Attendance from Users,Course,StudentGrade where Users.UserID=StudentGrade.StudentID AND Course.CourseID=StudentGrade.CourseID  AND StudentGrade.CourseID='" + cid + "'", con);
            dt1 = new DataTable();
            sda1.Fill(dt1);
            dGrid.ItemsSource = dt1.DefaultView;

        }


    }
}
