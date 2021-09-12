using System;
using System.Windows.Controls;
using System.Data.SqlClient;
using SchoolManagementSystem.ViewModels;
using System.Data;
using System.Configuration;
using SchoolManagementSystem.Models;
using System.Windows;
using System.Data.OleDb;
using DataGrid = System.Windows.Controls.DataGrid;
namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for CourseListScreen2.xaml
    /// </summary>
    public partial class CourseListScreen2 : Page
    {
        CourseViewModel course = new CourseViewModel();
        private bool isUpdateMode;
        private SchoolMSEntities1 ty = new SchoolMSEntities1();

        public CourseListScreen2 ()
        {
            InitializeComponent();


            // The DataContext serves as the starting point of Binding Paths
            DataContext = course;
            FillDataGrid();

        }

        private void Window_Loaded ( object sender, RoutedEventArgs e )
        {
            course.GetAll();
        }
        public void Load ()
        {
            //course.GetAll();
            FillDataGrid();
        }

        //MARK: fill datagrid need for the selection
        private void FillDataGrid ()

        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                SqlCommand command = new SqlCommand(
                   "select * from Course", connection);
                connection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(command);

                DataTable dt = new DataTable("Courses");

                sda.Fill(dt);

                usersDataGrid.ItemsSource = dt.DefaultView;
                //usersDataGrid.ItemsSource = course.AllCourses;

                usersDataGrid.Items.Refresh();

            }

        }
        private void usersDataGrid_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                //MessageBox.Show("check");

                courseIDTextBox.Text = row_selected["CourseID"].ToString();
                courseCodeTextBox.Text = row_selected["CourseCode"].ToString();
                courseNameTextBox.Text = row_selected["CourseName"].ToString();
                descriptionTextBox.Text = row_selected["Description"].ToString();
                examDateDatePicker.Text = row_selected["ExamDate"].ToString();
            }


        }





        private void DeleteCourse_Click ( object sender, RoutedEventArgs e )
        {
            //delete button 
            if (System.Windows.MessageBox.Show("Confirm delete of this record?", "Course", MessageBoxButton.YesNo)
            == MessageBoxResult.Yes)
            {
                if (courseIDtEmpty())
                {
                    try

                    {
                        if (course.CheckCourseID(Convert.ToInt32(courseIDTextBox.Text)))
                        {
                            course.DeleteCourse((courseCodeTextBox.Text));

                        }
                        else
                        {
                            System.Windows.MessageBox.Show("ID not existed");
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }

                    finally
                    {
                        FillDataGrid();
                        Clear();
                    }
                }
            }

        }

        private void Reset_Click ( object sender, RoutedEventArgs e )
        {
            Clear();

        }

        public void Clear ()
        {
            courseIDTextBox.Text = "";
            courseCodeTextBox.Text = "";
            courseNameTextBox.Text = "";
            descriptionTextBox.Text = "";
            examDateDatePicker.Text = "";
        }


        private void UpdateCourse_Click ( object sender, RoutedEventArgs e )
        {
            if (courseIDtEmpty())
            {
                if (course.CheckCourseID(Convert.ToInt32(courseIDTextBox.Text.Trim())))
                {
                    try
                    {

                        course.UpdateCourse(Convert.ToInt32(courseIDTextBox.Text.Trim()),
                                     courseNameTextBox.Text.Trim(),
                                     courseCodeTextBox.Text.Trim(),
                                     descriptionTextBox.Text.Trim(),
                                     Convert.ToDateTime(examDateDatePicker.Text)
                                    );
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        Load();
                        Clear();

                    }
                }

                else
                {
                    System.Windows.MessageBox.Show("Invalid course Id");

                }

            }
            else
            {
                System.Windows.MessageBox.Show("Please enter the course Id");

            }

        }

        private void AddNewCourse_Click ( object sender, RoutedEventArgs e )
        {
            if (courseCodeTextBox.Text != "")
            {
                //add button 
                try
                {
                    if (course.checkCourseCode(courseCodeTextBox.Text.Trim().ToString()))
                    {
                        System.Windows.MessageBox.Show("Course Already Exists");

                    }
                    else
                    {
                        course.AddCourse(courseNameTextBox.Text.Trim(),
                                         courseCodeTextBox.Text.Trim(),
                                         descriptionTextBox.Text.Trim(),
                                         Convert.ToDateTime(examDateDatePicker.Text)
                                         );
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
                finally
                {
                    Load();
                    Clear();

                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please fill the fields");

            }

        }

        public bool courseIDtEmpty ()
        {
            if (courseIDTextBox.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void getCourseByID ()
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Course WHERE CourseCode='" + courseCodeTextBox.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable(); //store data in dt

                da.Fill(dt);

                if (dt.Rows.Count >= 1) //check if id exist -- got one record
                {

                    courseIDTextBox.Text = Convert.ToInt32(dt.Rows[0]["CourseID"]).ToString();
                    courseNameTextBox.Text = dt.Rows[0]["CourseName"].ToString();
                    courseCodeTextBox.Text = dt.Rows[0]["CourseCode"].ToString();
                    descriptionTextBox.Text = dt.Rows[0]["Description"].ToString();
                    examDateDatePicker.Text = Convert.ToDateTime(dt.Rows[0]["ExamDate"]).ToString();

                }
                else
                {
                    System.Windows.MessageBox.Show("Invalid Course ID");


                }
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);

            }
        }

        private void find_button_Click ( object sender, RoutedEventArgs e )
        {
            getCourseByID();
        }

        //For Upload File
        private void btnBrowse_Click ( object sender, RoutedEventArgs e )
        {
            System.Windows.Forms.OpenFileDialog fdlg = new System.Windows.Forms.OpenFileDialog();
            fdlg.Title = "Select File";
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel Sheet (*.xls)|*.xls|All Files(*.*)|*.*|Excell|*.xlsx";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;


            if (fdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;
            }
        }


        // To read the excel data into datagridView
        private void btnImport_Click ( object sender, RoutedEventArgs e )
        {
            try
            {
                string constr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName.Text + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"";

                OleDbConnection theConnection = new OleDbConnection(constr);
                theConnection.Open();

                OleDbDataAdapter theDataAdapter = new OleDbDataAdapter("Select * from[sheet1$]", theConnection);
                DataSet theSD = new DataSet();
                DataTable dt = new DataTable();

                theDataAdapter.Fill(dt);
                this.usersDataGrid.ItemsSource = dt.DefaultView;
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void savetry2_Click ( object sender, RoutedEventArgs e )
        {
            string CourseName = "";
            string CourseCode = "";
            string Description = "";
            string ExamDate = "";

            try
            {

                string constr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName.Text + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"";
                OleDbConnection Econ = new OleDbConnection(constr);

                string Query = string.Format("Select * FROM [{0}]", "Sheet1$");
                OleDbCommand Ecom = new OleDbCommand(Query, Econ);
                Econ.Open();

                OleDbDataReader o_dr = Ecom.ExecuteReader();

                while (o_dr.Read())
                {
                    CourseName = o_dr[1].ToString();
                    CourseCode = o_dr[2].ToString();
                    Description = o_dr[3].ToString();
                    ExamDate = Convert.ToDateTime(o_dr[4]).ToString();

                    course.UpdateCourse1(
                                                    CourseName,
                                                    CourseCode,
                                                    Description,
                                                    Convert.ToDateTime(ExamDate)
                                                 );
                }

                MessageBox.Show("Data Have Been Updated!!");
                FillDataGrid();

                Econ.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}