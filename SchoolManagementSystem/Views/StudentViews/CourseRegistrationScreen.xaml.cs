using SchoolManagementSystem.Models;
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

namespace SchoolManagementSystem.Views.StudentViews
{
    /// <summary>
    /// Interaction logic for CourseRegistrationScreen.xaml
    /// </summary>
    public partial class CourseRegistrationScreen : Page
    {
        TimeTableViewModel table = new TimeTableViewModel();
        StudentGradeViewModel grade = new StudentGradeViewModel();
        public static int courseID;
        public static int sectionID;

        public CourseRegistrationScreen ()
        {
            InitializeComponent();
            FillDataGrid();
            fillCourseBox();


        }

        private void Register_Click ( object sender, RoutedEventArgs e )
        {
            DateTime now = DateTime.Now;

            if (notEmpty())
            {

                table.InsertTimeTable(
                                    Convert.ToInt32(UserViewModel.userSession.UserID),
                                    courseID,
                                    roomNotxt.Text.ToString(),
                                    2021,
                                    teachernametxt.Text.ToString(),
                                    coursenametxt.Text.ToString(),
                                    timetxt.Text.ToString(),
                                    course_combo_box.Text.ToString(),
                                    Convert.ToInt32(sectionnotxt.Text.ToString()),
                                    Convert.ToDateTime(examDatetxt.Text.ToString()),
                                    sectionID
                                   );

                grade.AddStudentGrade(courseID,
                                      Convert.ToInt32(UserViewModel.userSession.UserID),
                                      0,
                                      0,
                                      false,
                                      2021,
                                      sectionID);
                FillDataGrid();

            }
            else
            {
                MessageBox.Show("Please Select a course!!");
            }
        }


        private void FillDataGrid ()

        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                SqlCommand command = new SqlCommand(
                     "select TimeTable.CourseCode,TimeTable.CourseName,TimeTable.SectionNo,TimeTable.RoomNo, TimeTable.Time, Course.Examdate,TimeTable.TeacherName from TimeTable, Course, Section where Course.CourseID=TimeTable.CourseID AND Course.CourseID=Section.CourseID AND Section.SectionID =TimeTable.SectionID AND UserID ='" + UserViewModel.userSession.UserID + "'", connection);
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

        public void fillSectionComboBox ()
        {
            if (courseID >0)
            {
                try
                {

                    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("select SectionNum, SectionID, CourseID from Section where CourseID = '" + courseID + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt); //db have all the courses names

                    sectionno_combo_box.ItemsSource = dt.DefaultView;
                    sectionno_combo_box.SelectedIndex = -1;
                    sectionno_combo_box.DisplayMemberPath = "SectionNum";
                    sectionno_combo_box.SelectedValuePath = "SectionID";

                    con.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            }
        }



        //MARK: to fill the course detail group box with the selected course code
        public void fillCourseDetails ()
        {
            if (courseID > 0 && sectionID > 0)
            {
                try
                {

                    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                    using (SqlConnection connection = new SqlConnection(strcon))
                    {
                        SqlCommand cmd = new SqlCommand("select TeacherCourses.TeacherName, Course.CourseCode, Course.CourseID ,Course.CourseName,Course.ExamDate, Section.SectionNum, Section.Time, Room.RoomNum from Course, Section, Room, TeacherCourses where Section.CourseID= Course.CourseID AND Section.RoomID= Room.RoomID AND TeacherCourses.CourseID=Course.CourseID AND Course.CourseCode='" + course_combo_box.Text.ToString() + "'AND Section.SectionNum='" + sectionno_combo_box.Text.ToString() + "'", connection);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);


                        if (dt.Rows.Count > 0)
                        {
                            coursenametxt.Text = dt.Rows[0]["CourseName"].ToString();
                            examDatetxt.Text = dt.Rows[0]["ExamDate"].ToString();
                            sectionnotxt.Text = dt.Rows[0]["SectionNum"].ToString();
                            timetxt.Text = dt.Rows[0]["Time"].ToString();
                            roomNotxt.Text = dt.Rows[0]["RoomNum"].ToString();
                            teachernametxt.Text = dt.Rows[0]["TeacherName"].ToString();
                            txtcourseID.Text = dt.Rows[0]["CourseID"].ToString();

                        }
                        connection.Close();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }


        //MARK: get course id
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

        //MARK: Get Section Id
        public void getSectionIDD ()
        {

            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT SectionNum,SectionID from Section where SectionNum='" + sectionno_combo_box.Text.ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the courses names

                sectionID = Convert.ToInt32(dt.Rows[0]["SectionID"].ToString());

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        private void cancel_Click ( object sender, RoutedEventArgs e )
        {
            Clear();
        }

        public void Clear()
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
            sectionno_combo_box.Text = "";
            sectionno_combo_box.SelectedItem = "";
        }
        public bool notEmpty ()
        {
            if(course_combo_box.Text == "" && sectionno_combo_box.Text == "")
            { return false; }
            else { return true; }
        }

        private void course_combo_box_DropDownClosed ( object sender, EventArgs e )
        {
            if (course_combo_box.Text != "")
            {
                getcourseIDD();
                fillSectionComboBox();

            }
            else
            {
                Clear();

            }
            if (sectionno_combo_box.Text == "")
            {
                coursenametxt.Text = "";
                examDatetxt.Text = "";
                sectionnotxt.Text = "";
                timetxt.Text = "";
                roomNotxt.Text = "";
                teachernametxt.Text = "";
                txtcourseID.Text = "";
                sectionno_combo_box.Text = "";
                sectionno_combo_box.SelectedItem = "";
            }
            course_details_groupbox.Visibility = Visibility.Visible;


        }

        private void sectionno_combo_box_DropDownClosed ( object sender, EventArgs e )
        {
            if (sectionno_combo_box.Text != "")
            {
                getSectionIDD();
                fillCourseDetails();

            }
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
                    grade.deleteStudentGrade(courseID,
                                      Convert.ToInt32(UserViewModel.userSession.UserID),
                                      sectionID);

                    FillDataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    FillDataGrid();
                }
            }
        }

        private void sectionno_combo_box_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {

        }
    }
}
