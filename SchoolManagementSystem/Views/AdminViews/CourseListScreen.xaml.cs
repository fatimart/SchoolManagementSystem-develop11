using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using SchoolManagementSystem.ViewModels;
using System.Data;
using System.Collections;
using System.Configuration;

namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for CourseListScreen.xaml
    /// </summary>
    public partial class CourseListScreen : Page
    {
        CourseViewModel course = new CourseViewModel();
        public CourseListScreen()
        {
            InitializeComponent();
               

            // The DataContext serves as the starting point of Binding Paths
            DataContext = course;
            FillDataGrid();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            course.GetAll();
        }
        public void Load()
        {
            course.GetAll();
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

        

        public IEnumerable<DataGridRow> GetDataGridRows ( DataGrid grid )
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        
        

        private void DeleteCourse_Click ( object sender, RoutedEventArgs e )
        {
            //delete button 
            if (MessageBox.Show("Confirm delete of this record?", "Course", MessageBoxButton.YesNo)
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
                            MessageBox.Show("ID not existed");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
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
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        Load();
                        Clear();

                    }
                }

                else
                {
                    MessageBox.Show("Invalid course Id");

                }

            }
            else
            {
                MessageBox.Show("Please enter the course Id");

            }

        }

        private void AddNewCourse_Click ( object sender, RoutedEventArgs e )
        {
            if (courseCodeTextBox.Text != "" )
            {
                //add button 
                try
                {
                    if (course.checkCourseCode(courseCodeTextBox.Text.Trim().ToString()))
                    {
                        MessageBox.Show("Course Already Exists");

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
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Load();
                    Clear();

                }
            }
            else
            {
                MessageBox.Show("Please fill the fields");

            }

        }

        public bool courseIDtEmpty ()
        {
            if (courseIDTextBox.Text !="")
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
                    MessageBox.Show("Invalid user ID");


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void find_button_Click ( object sender, RoutedEventArgs e )
        {
            getCourseByID();
        }
    }
}
