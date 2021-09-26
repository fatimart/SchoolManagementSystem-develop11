using Microsoft.Win32;
using SchoolManagementSystem.ViewModels;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for UploadCourseScreen.xaml
    /// </summary>
    public partial class UploadCourseScreen : Page
    {
        CourseViewModel courseViewModel = new CourseViewModel();

        public UploadCourseScreen ()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click ( object sender, RoutedEventArgs e )
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select File";
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel Sheet (*.xls)|*.xls|All Files(*.*)|*.*|Excell|*.xlsx";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == true)
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
                this.datagridview.ItemsSource = dt.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //reload the data and fill it into the datagridview
        public void fillGrid ()
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter("SELECT * from Course", con);
                DataTable dt = new DataTable(); //store data in dt
                da.Fill(dt);

                datagridview.ItemsSource = dt.DefaultView;
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        //save data gridview to database
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

                    //courseViewModel.UpdateCourseDetails(
                    //                                CourseName,
                    //                                CourseCode,
                    //                                Description,
                    //                                Convert.ToDateTime(ExamDate)
                    //                             );
                }

                MessageBox.Show("Data Have Been Updated!!");
                fillGrid();

                Econ.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

      
 