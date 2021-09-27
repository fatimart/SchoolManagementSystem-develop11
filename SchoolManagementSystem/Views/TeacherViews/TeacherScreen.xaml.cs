using SchoolManagementSystem.ModelEntity;
using SchoolManagementSystem.ViewModels;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;


namespace SchoolManagementSystem.Views.TeacherViews
{
    /// <summary>
    /// Interaction logic for TeacherScreen.xaml
    /// </summary>
    public partial class TeacherScreen : Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
        StudentGradeViewModel viewmodel = new StudentGradeViewModel();

        public TeacherScreen ()
        {

            InitializeComponent();
            DataFill();
            CourseComboBox();
            comboBoxValue();
            DataContext = viewmodel;
        }

        DataSet ds;
        SqlDataAdapter dataAdapter;
        SqlDataAdapter sda1;
        DataTable dt1;
        SqlCommandBuilder Scb;





        public void DataFill ()
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
                try
                {
                    SqlCommand command = new SqlCommand(
                         "select Course.CourseID,Course.CourseName,Course.CourseCode,Section.SectionNum,Section.Time from Course, Section, TeacherCourses where TeacherCourses.UserID='" + UserViewModel.userSession.UserID + "'And Course.CourseID=TeacherCourses.CourseID AND Section.CourseID=TeacherCourses.CourseID AND Course.CourseID=Section.CourseID", connection);

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
                         "select StudentGrade.ID,StudentGrade.StudentID,StudentGrade.Score,StudentGrade.Attendance,StudentGrade.Done from Course,StudentGrade where Course.CourseID=StudentGrade.CourseID AND StudentGrade.CourseID='" + cid + "'", connection);
                    dataAdapter = new SqlDataAdapter(command);


                    dataAdapter.Fill(dt1);
                    dGrid.ItemsSource = dt1.DefaultView;

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
            var CID = course_combo_box.Text.Split('-');
            string cid = CID[0];
            return cid;
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

                SqlCommand cmd = new SqlCommand("select Concat(Course.CourseID,'-',Course.CourseCode,'-Section-',Section.SectionNum) AS CodeSection from Course, Section,TeacherCourses where TeacherCourses.UserID = '" + UserViewModel.userSession.UserID + "'AND Course.CourseID = Section.CourseID AND TeacherCourses.SectionID=Section.SectionID", con);
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

        private void course_combo_box_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {

        }
        private void Course_combo_box_DropDownClosed ( object sender, EventArgs e )
        {


        }

        private void open_btn_Click ( object sender, RoutedEventArgs e )
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
            string cCode = CID[1];
            string CSection = CID[3];
            SqlConnection con = new SqlConnection(strcon);
            sda1 = new SqlDataAdapter("select StudentGrade.ID,StudentGrade.StudentID,Users.Name,StudentGrade.Score,StudentGrade.Attendance,StudentGrade.Done from StudentGrade, TimeTable, Users, Section where Users.UserID = TimeTable.UserID AND TimeTable.UserID = StudentGrade.StudentID AND Users.UserID = StudentGrade.StudentID AND TimeTable.SectionID = StudentGrade.SectionID AND TimeTable.SectionID = Section.SectionID AND Section.SectionID = StudentGrade.SectionID AND TimeTable.CourseID = StudentGrade.CourseID AND TimeTable.CourseID = Section.CourseID AND Section.CourseID = StudentGrade.CourseID AND StudentGrade.CourseID='" + cid + "'", con);
            dt1 = new DataTable();
            sda1.Fill(dt1);
            dGrid.ItemsSource = dt1.DefaultView;


        }
        private void Button_Click ( object sender, RoutedEventArgs e )
        {//save

            if (CheckComboBox())
            {
                Save();
                CourseDataFill1();
            }
            else { return; }




        }
        public void Save ()
        {
            for (int i = 0; i < dGrid.Items.Count - 1; i++)
            {
                DataGridCell ID = GetCell(i, 0);
                DataGridCell StudentID = GetCell(i, 1);
                DataGridCell score = GetCell(i, 3);
                DataGridCell attendance = GetCell(i, 4);
                DataGridCell Done = GetCell(i, 5);
                TextBlock tb = ID.Content as TextBlock;
                TextBlock tb1 = StudentID.Content as TextBlock;
                TextBlock tb2 = score.Content as TextBlock;
                TextBlock tb3 = attendance.Content as TextBlock;
                CheckBox tb4 = Done.Content as CheckBox;

                StudentGrade Sgrades = new StudentGrade
                {
                    ID = Convert.ToInt32(tb.Text),
                    StudentID = Convert.ToInt32(tb1.Text),
                    Score = Convert.ToInt32(tb2.Text),
                    Attendance = Convert.ToInt32(tb3.Text),
                    Done = Convert.ToBoolean(tb4.IsChecked.Value.ToString()),
                };
                StudentGradeViewModel SGVM = new StudentGradeViewModel();
                //SGVM.UpdateStudentGrade1(
                //                       Sgrades.ID,
                //                       Sgrades.StudentID,
                //                       Sgrades.Score,
                //                       Sgrades.Attendance,
                //                       Sgrades.Done
                //                    );
            }
            MessageBox.Show("The Database Updated succesfully");

        }

        public DataSet CreateCommandAndUpdate ()
        {

            DataSet dataSet = new DataSet();
            var CID = course_combo_box.Text.Split('-');
            string cid = CID[0];
            try
            {
                using (OleDbConnection connection =
                       new OleDbConnection(@"Data Source = PIE-RD-DESK-079\FADHELWORK2; Initial Catalog = SchoolMS; Provider = MSOLEDBSQL;providerName = System.Data.SqlClientUser; Id=sa;Password=Fad4work;"))
                {
                    connection.Open();
                    OleDbDataAdapter adapter =
                        new OleDbDataAdapter();
                    adapter.SelectCommand =
                        new OleDbCommand("select Distinct ID from Course, StudentGrade where Course.CourseID = StudentGrade.CourseID AND StudentGrade.CourseID = '" + cid + "'", connection);
                    OleDbCommandBuilder builder =
                        new OleDbCommandBuilder(adapter);

                    adapter.Fill(dataSet);

                    // Code to modify data in the DataSet here.

                    // Without the OleDbCommandBuilder, this line would fail.
                    adapter.UpdateCommand = builder.GetUpdateCommand();
                    adapter.Update(dataSet);
                    return dataSet;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return dataSet;
            }

        }

        public void UpdateDataGrid ()
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
                try
                {

                    var CID = course_combo_box.Text.Split('-');
                    string cid = CID[0];
                    string cCode = CID[1];
                    string CSection = CID[3];
                    SqlCommand command = new SqlCommand(
                         "select StudentGrade.ID,StudentGrade.StudentID,StudentGrade.Score,StudentGrade.Attendance,StudentGrade.Done from Course,StudentGrade where Course.CourseID=StudentGrade.CourseID AND StudentGrade.CourseID='" + cid + "'", connection);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    dGrid.ItemsSource.Cast<object>().ToList();

                    for (int i = 0; i < dGrid.Items.Count; i++)
                    {
                        DataGridRow row = (DataGridRow)dGrid.ItemContainerGenerator.ContainerFromIndex(i);
                        foreach (var gridColumn in dGrid.Columns)
                        {
                            if (gridColumn.Header == "StudentID")
                            {
                                MessageBox.Show("StudentID");
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.ToString());
                }
        }
        private void DataGrid_RowEditEnding ( object sender, DataGridRowEditEndingEventArgs e )
        {


        }

        public DataGridCell GetCell ( int row, int column )
        {
            DataGridRow rowData = GetRow(row);
            if (rowData != null)
            {
                DataGridCellsPresenter cellPresenter = GetVisualChild<DataGridCellsPresenter>(rowData);
                DataGridCell cell = (DataGridCell)cellPresenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    dGrid.ScrollIntoView(rowData, dGrid.Columns[column]);
                    cell = (DataGridCell)cellPresenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        public DataGridRow GetRow ( int index )
        {
            DataGridRow row = (DataGridRow)dGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                dGrid.UpdateLayout();
                dGrid.ScrollIntoView(dGrid.Items[index]);
                row = (DataGridRow)dGrid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static T GetVisualChild<T> ( Visual parent ) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private void Button_Click_1 ( object sender, RoutedEventArgs e )
        {//Upload

            if (CheckComboBox())
            {
                var CID = course_combo_box.Text.Split('-');
                string cid = CID[0];
                FileUpload1 FUpload = new FileUpload1(cid);
                FUpload.Owner = Application.Current.MainWindow;
                FUpload.Show();
            }
            else { return; }

        }
    }
}
