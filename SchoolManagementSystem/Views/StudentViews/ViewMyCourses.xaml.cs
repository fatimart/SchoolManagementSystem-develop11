using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SchoolManagementSystem.Views.StudentViews
{
    /// <summary>
    /// Interaction logic for ViewMyCourses.xaml
    /// </summary>
    public partial class ViewMyCourses : Page
    {
        private Course objEmpToEdit;
        private bool isUpdateMode;
        bool isInsertMode = false;
        bool isBeingEdited = false;
        SchoolMSEntities1 context = new SchoolMSEntities1();

        SchoolMSEntities1 objContext = new SchoolMSEntities1();
        CourseViewModel courseViewModel = new CourseViewModel();

        private static string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        SqlConnection conn = new SqlConnection(strcon);
        private SqlDataAdapter adapter;

        public ViewMyCourses ()
        {
            InitializeComponent();
            //DataContext = courseViewModel;
            //dgEmp.ItemsSource = objContext.Courses;

            DataSet ds = new DataSet();
            dgEmp.BeginInit();
            ds.Tables.Add(CreateTable());
            dgEmp.DataContext = ds.Tables[0];

            dgEmp.Items.Refresh();
            dgEmp.EndInit();

        }

        private void Window_Loaded ( object sender, RoutedEventArgs e )
        {
            //SchoolMSEntities1 objContext = new SchoolMSEntities1();
            //dgEmp.ItemsSource = objContext.Courses.ToList();

        }



        //submit button click，update DB
        private void SubmitButton_Click ( object sender, RoutedEventArgs e )
        {
            DataTable dt = dgEmp.DataContext as DataTable;
            SqlCommandBuilder com = new SqlCommandBuilder(adapter);
            adapter.Update(dt);
        }


        public DataTable CreateTable ()
        {
            string query = "Select * from Course";
            adapter = new SqlDataAdapter(query, conn);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;

        }


























        private void dgEmp_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            objEmpToEdit = dgEmp.SelectedItem as Course;
        }

        private void btnUpdate_Click ( object sender, RoutedEventArgs e )
        {
             isUpdateMode = true;
            dgEmp.Columns[1].IsReadOnly = false;
            objContext.SaveChanges();

        }

        private void dgEmp_CellEditEnding ( object sender, DataGridCellEditEndingEventArgs e )
        {

            if (isUpdateMode) //The Row is edited
            {
                Course TempEmp = (from emp in objContext.Courses
                                    where emp.CourseID == objEmpToEdit.CourseID
                                    select emp).First();


                FrameworkElement element_1 = dgEmp.Columns[1].GetCellContent(e.Row);
                if (element_1.GetType() == typeof(TextBox))
                {
                    var xxSalary = ((TextBox)element_1).Text;
                    objEmpToEdit.CourseName = xxSalary.ToString();
                    MessageBox.Show(xxSalary);

                }
                MessageBox.Show(((TextBox)element_1).Text);


            }

        }

        //private void dgEmp_RowEditEnding ( object sender, DataGridRowEditEndingEventArgs e )
        //{
        //    objContext.SaveChanges();
        //    MessageBox.Show("The Current row updation is complete..");
        //}

        private void btnDelete_Click ( object sender, RoutedEventArgs e )
        {
            
        }

        private void dgEmp_RowEditEnding ( object sender, DataGridRowEditEndingEventArgs e )
        {
            Course employee = new Course();
            Course emp = e.Row.DataContext as Course;
            if (isInsertMode)
            {
                var InsertRecord = MessageBox.Show("Do you want to add " + emp.CourseName + " as a new course?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (InsertRecord == MessageBoxResult.Yes)
                {
                    //employee.CourseCode = emp.CourseCode;
                    //employee.CourseName = emp.CourseName;
                    //employee.Description = emp.Description;
                    //employee.ExamDate = emp.ExamDate;
                    //context.Courses.Add(employee);
                    //context.SaveChanges();
                    try
                    {
                        courseViewModel.UpdateCourse(emp.CourseID,emp.CourseCode.Trim(), emp.CourseName.Trim(), emp.Description.Trim(), Convert.ToDateTime(emp.ExamDate));
                        dgEmp.ItemsSource = courseViewModel.AllCourses;
                        MessageBox.Show(employee.CourseName + " " + employee.CourseCode + " has being added!", "Inserting Record", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }
                else
                    dgEmp.ItemsSource = courseViewModel.AllCourses;
            }
            context.SaveChanges();
        }

        private void dgEmp_PreviewKeyDown ( object sender, KeyEventArgs e )
        {
            if (e.Key == Key.Delete && !isBeingEdited)
            {
                var grid = (DataGrid)sender;
                if (grid.SelectedItems.Count > 0)
                {
                    var Res = MessageBox.Show("Are you sure you want to delete " + grid.SelectedItems.Count + " Employees?", "Deleting Records", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (Res == MessageBoxResult.Yes)
                    {
                        try

                        {
                            foreach (var row in grid.SelectedItems)
                            {
                                Course employee = row as Course;
                                context.Courses.Remove(employee);
                            }
                            context.SaveChanges();
                            MessageBox.Show(grid.SelectedItems.Count + " Employees have being deleted!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }
                    }
                    else
                        dgEmp.ItemsSource = courseViewModel.AllCourses;
                }
            }
        }

        private void dgEmp_AddingNewItem ( object sender, AddingNewItemEventArgs e )
        {
            isInsertMode = true;
        }

        private void dgEmp_BeginningEdit ( object sender, DataGridBeginningEditEventArgs e )
        {
            isBeingEdited = true;
        }

    }
}
