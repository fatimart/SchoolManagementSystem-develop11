using SchoolManagementSystem.Utilities;
using SchoolManagementSystem.ViewModels;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for SectionScreen.xaml
    /// </summary>
    public partial class SectionScreen : Page
    {

        SectionViewModel sectionViewModel = new SectionViewModel();
        InitielizeHttpClient initielizeHttpClient = new InitielizeHttpClient();
        public static  int courseID;
        public static int roomID;

        public SectionScreen ()
        {
            InitializeComponent();
            initielizeHttpClient.InitielizeClient();

            fillCourseBox();
            fillSectioneBox();
            FillDataGrid();
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();

        }

        //Add
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (fillNotEmpty())
            {
                try
                {
                    sectionViewModel.CreateNewSection((Convert.ToInt32(sectionnumtxtbox.Text)),
                                                courseID,
                                                roomID,
                                                timetxtbox.Text.Trim().ToString()
                                                  );

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    //Clear();
                    FillDataGrid();

                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields");

            }
        }

        //Update
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            if (fillNotEmpty())
            {

                try
                {
                    //if (sectionViewModel.checkSectionExists(Convert.ToInt32(sectionnumtxtbox.Text), courseID))
                    //{
                        sectionViewModel.UpdateSectionDetails((Convert.ToInt32(sectionnumtxtbox.Text)),
                                                    courseID,
                                                    roomID,
                                                    timetxtbox.Text.Trim().ToString()
                                                      );
                    //}
                    //else
                    //{
                    //    MessageBox.Show("course with section number " + Convert.ToInt32(sectionnumtxtbox.Text) + " does not exists!");

                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    //Clear();
                    FillDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields");

            }
            
        }

        //Delete
        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Confirm delete of this record?", "Section", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                if (fillNotEmpty())
                {
                    
                        sectionViewModel.DeleteSectionDetails((Convert.ToInt32(sectionnumtxtbox.Text)), courseID);
                   
                        //Clear();
                        FillDataGrid();
                   
                }
                else
                {
                    MessageBox.Show("Please fill all the fields of course code and section num");

                }
            }
        }

        //Clear textbox e
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        public void fillCourseBox ()
        {
            try
            {
                course_code_combobox.Items.Clear();

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
                roomNo_combobox.Items.Clear();

                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT RoomNum,RoomID from Room;", con);
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

        public void getroomIDD()
        {

            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT RoomNum,RoomID from Room where RoomNum='" + roomNo_combobox.Text.ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the courses names

                roomID = Convert.ToInt32(dt.Rows[0]["RoomID"].ToString());

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

                SqlCommand cmd = new SqlCommand("SELECT CourseCode,CourseID from Course where CourseCode='" + course_code_combobox.Text.ToString() + "'", con);
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

        private void FillDataGrid ()

        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                SqlCommand command = new SqlCommand(
                   "select Section.SectionID, Section.Time, Course.CourseCode, Section.SectionNum, Room.RoomNum from Section,Course,Room where Section.CourseID=Course.CourseID AND Section.RoomID=Room.RoomID", connection);
                connection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(command);

                DataTable dt = new DataTable("Section");

                sda.Fill(dt);
                sectionDataGrid.ItemsSource = dt.DefaultView;

                //usersDataGrid.ItemsSource = _userViewModel.AllSections;

                sectionDataGrid.Items.Refresh();

            }

        }

        private void course_code_combobox_DropDownClosed ( object sender, EventArgs e )
        {
            if (course_code_combobox.Text != "")
            {
                getcourseIDD();
            }
        }

        private void roomNo_combobox_DropDownClosed ( object sender, EventArgs e )
        {
            if (roomNo_combobox.Text != "")
            {
                getroomIDD();
            }
        }

        private bool fillNotEmpty()
        {
            if(course_code_combobox.Text != "" && roomNo_combobox.Text != "" && sectionnumtxtbox.Text != "" && timetxtbox.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            course_code_combobox.Text = "";
            roomNo_combobox.Text = "";
            sectionnumtxtbox.Text = "";
            timetxtbox.Text = "";
        }

        private void sectionDataGrid_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                course_code_combobox.Text = row_selected["CourseCode"].ToString();
                roomNo_combobox.Text = row_selected["RoomNum"].ToString();
                sectionnumtxtbox.Text = row_selected["SectionNum"].ToString();
                timetxtbox.Text = row_selected["Time"].ToString();

            }
            getcourseIDD();
            getroomIDD();
        }
    }
}
