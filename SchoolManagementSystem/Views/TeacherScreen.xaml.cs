using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
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

namespace SchoolManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for TeacherScreen.xaml
    /// </summary>
    public partial class TeacherScreen : Page
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();

        public TeacherScreen()
        {
            InitializeComponent();
            DataFill();
            CourseComboBox();
            comboBoxValue();
        }
        DataSet ds;
        OleDbDataAdapter dataAdapter;
        SqlDataAdapter da;
        DataTable dt;
        void ReadData()
        {
            var CID = course_combo_box.Text.Split('-');
            string cid = CID[0];
            this.ds = new DataSet();
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            dataAdapter = new OleDbDataAdapter("select StudentGrade.StudentID,StudentGrade.Score,StudentGrade.Attendance,StudentGrade.Done from Course,StudentGrade where Course.CourseID=StudentGrade.CourseID AND StudentGrade.CourseID='" + cid + "'", strcon);
            this.dataAdapter.Fill(this.ds, "TABLE1");
            this.ds.AcceptChanges();
            //set the table as the datasource for the grid in order to show that data in the grid
            this.dGrid.ItemsSource = ds.DefaultViewManager;
        }





        public void DataFill()
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
        public void CourseDataFill()
        {

            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
                try
                {
                    this.ds = new DataSet();
                    var CID = course_combo_box.Text.Split('-');
                    string cid = CID[0];
                    SqlCommand command = new SqlCommand(
                         "select StudentGrade.StudentID,StudentGrade.Score,StudentGrade.Attendance,StudentGrade.Done from Course,StudentGrade where Course.CourseID=StudentGrade.CourseID AND StudentGrade.CourseID='" + cid + "'", connection);
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


        public string comboBoxValue()
        {
            string S = course_combo_box.Text;
            return S;
        }
        public void CourseComboBox()
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
             //    var CID = course_combo_box.Text.Split('-');
            
               



                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        private void course_combo_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Course_combo_box_DropDownClosed(object sender, EventArgs e)
        {


        }

        private void open_btn_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show(comboBoxValue());
            layout layout = new layout();
            CourseDataFill();
            //  Button_Click.Visibility = Visibility.Visible;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ReadData();
           // DataFill();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {//save
       
        }
    }
}
