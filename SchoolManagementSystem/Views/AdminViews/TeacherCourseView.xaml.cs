using SchoolManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
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
        public static int courseID;
        public static int sectionID;
        public static int teacherID;


        public TeacherCourseView ()
        {
            InitializeComponent();
            FillCourseBox();
            FillTeacherBox();
            FillCourseDetails();
        }

        public void FillCourseBox ()
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
        public void fillSectionComboBox ()
        {
            if (courseID > 0)
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

                    section_combo_box.ItemsSource = dt.DefaultView;
                    section_combo_box.SelectedIndex = -1;
                    section_combo_box.DisplayMemberPath = "SectionNum";
                    section_combo_box.SelectedValuePath = "SectionID";

                    con.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

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

                SqlCommand cmd = new SqlCommand("SELECT SectionNum,SectionID from Section where SectionNum='" + section_combo_box.Text.ToString() + "'", con);
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




        public void FillTeacherBox ()
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("select Concat(UserID, '-', Name) AS TeacherName from Users where Type='" + "teacher" + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the teachers names

                teacher_combo_box.ItemsSource = dt.DefaultView;
                teacher_combo_box.SelectedIndex = -1;
                teacher_combo_box.DisplayMemberPath = "TeacherName";
                teacher_combo_box.SelectedValuePath = "UserID";


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
                    SqlCommand cmd = new SqlCommand("select TeacherCourses.TID, TeacherCourses.CourseCode, Section.SectionNum,TeacherCourses.TeacherName from TeacherCourses, Section where Section.SectionID=TeacherCourses.SectionID ", connection);
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
      //  Teacher course View Model


        private void Course_combo_box_DropDownClosed ( object sender, EventArgs e )
        {
            if (course_combo_box.Text != "")
            {
                getcourseIDD();
                fillSectionComboBox();

            }

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
            if (notEmpty())
            {
                var CID = teacher_combo_box.Text.Split('-');
                string user_id = CID[0];
                string teacher_name = CID[1];

                TeacherCourse.AddTeacherCourse(teacher_name,
                                               course_combo_box.Text.ToString(),
                                               courseID,
                                               sectionID,
                                               Convert.ToInt32(user_id));
                FillCourseDetails();
            }
            else
            {
                MessageBox.Show("Please Select a course, section number and teacher name!!");
            }
        }

        private void Button_Click_1 ( object sender, RoutedEventArgs e )
        {//update

            if (TIDTextBox.Text != "")
            {
                var CID = teacher_combo_box.Text.Split('-');
                string user_id = CID[0];
                string teacher_name = CID[1];

                if (TeacherCourse.checkifRecordTableExsist(courseID, sectionID))
                {
                    TeacherCourse.UpdateTeacherCourse(Convert.ToInt32(TIDTextBox.Text),
                                                  teacher_name,
                                                  course_combo_box.Text.ToString(),
                                                  courseID,
                                                  sectionID,
                                                  Convert.ToInt32(user_id));
                    FillCourseDetails();
                }
                else
                {
                    MessageBox.Show("No course found!!");

                }
            }
            else
            {
                MessageBox.Show("Please enter the tid !!");

            }
        }

        private void Button_Click_2 ( object sender, RoutedEventArgs e )
        {//delete
            var CID = teacher_combo_box.Text.Split('-');
            string user_id = CID[0];
            string teacher_name = CID[1];

            if (MessageBox.Show("Are You sure you want to delete ?", "Course", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (TIDTextBox.Text != "")
                {
                    try
                    {
                        TeacherCourse.DeleteTeacherCourse(courseID,
                                                          sectionID,
                                                          Convert.ToInt32(user_id));
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    finally
                    {
                        FillCourseDetails();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter the tid !!");

                }
                }
        }

        private void find_Click ( object sender, RoutedEventArgs e )
        {
            if (TIDTextBox.Text != "")
            {
                FindTeacherCourse(Convert.ToInt32(TIDTextBox.Text));
            }
            else
            {
                MessageBox.Show("Please write the section number!!");
            }
        }

        public void FindTeacherCourse ( int Tid )
        {
            try
            {

                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("select CourseCode,TeacherName from TeacherCourses where TID='" + Tid + "'", connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {
                        course_combo_box.SelectedValue = dt.Rows[0]["CourseCode"].ToString();
                        teacher_combo_box.SelectedValue = dt.Rows[0]["TeacherName"].ToString();
                    }
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void section_combo_box_DropDownClosed ( object sender, EventArgs e )
        {
            if (section_combo_box.Text != "")
            {
                getSectionIDD();
                

            }
        }

        public bool notEmpty ()
        {
            if (course_combo_box.Text == "" && section_combo_box.Text == "" && teacher_combo_box.Text == "")
            { return false; }
            else { return true; }
        } 

        private void clear_btn_Click ( object sender, RoutedEventArgs e )
        {
            clear();
        }

        public void clear()
        {
            TIDTextBox.Text = "";
            course_combo_box.SelectedItem = "";
            course_combo_box.Text = "";
            teacher_combo_box.Text = "";
            teacher_combo_box.SelectedItem = "";
            section_combo_box.Text = "";
            section_combo_box.SelectedItem = "";
        }
    }
}
