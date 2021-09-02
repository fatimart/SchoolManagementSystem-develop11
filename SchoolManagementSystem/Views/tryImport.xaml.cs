using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
namespace SchoolManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for tryImport.xaml
    /// </summary>
    public partial class tryImport : Page
    {
        private SchoolMSEntities1 ty = new SchoolMSEntities1();
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        CourseViewModel course = new CourseViewModel();
        public tryImport()
        {
            InitializeComponent();
        }
        public void FillDataGrid()
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "select File";
            fdlg.FileName = txtFilePath.Text;
            fdlg.DefaultExt = ".xlsx";
            fdlg.Filter = "(.xlsx)|*.xlsx";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == true)
            {
                txtFilePath.Text = fdlg.FileName;
            }

            string commandText = "SELECT * FROM [Sheet1$]";
            string oledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source=" + fdlg.FileName + ";" +
            "Extended Properties=\"Excel 12.0;HDR=YES\";";
            OleDbConnection connection = new OleDbConnection(oledbConnectString);
            OleDbCommand command = new OleDbCommand(commandText, connection);
            try{
                connection.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter Adpt = new OleDbDataAdapter(commandText, connection);
                Adpt.Fill(dt);
                dtGrid.ItemsSource = dt.DefaultView;
            }
         catch (Exception )
            {
            }


        }
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void btnimport_Click(object sender, RoutedEventArgs e)
        {
          
           OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "select File";
            fdlg.FileName = txtFilePath.Text;
            fdlg.DefaultExt = ".xlsx";
            fdlg.Filter = "(.xlsx)|*.xlsx";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == true)
            {
                txtFilePath.Text = fdlg.FileName;
            }

            string commandText = "SELECT * FROM [Sheet1$]";
            string oledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source=" + fdlg.FileName + ";" +
            "Extended Properties=\"Excel 12.0;HDR=YES\";";
            OleDbConnection connection = new OleDbConnection(oledbConnectString);
            OleDbCommand command = new OleDbCommand(commandText, connection);
            DataTable dt = new DataTable();
            OleDbDataAdapter Adpt = new OleDbDataAdapter(commandText, connection);
            OleDbDataReader reader;
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                Adpt.Fill(dt);
                dtGrid.ItemsSource = dt.DefaultView;
                while (reader.Read())
                {
                    
                    User user1 = new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString().Trim(),
                        Name = reader["Name"].ToString().Trim(),
                        Email = reader["Email"].ToString().Trim(),
                        CPR = Convert.ToDecimal(reader["CPR"]),
                        Address = reader["Address"].ToString().Trim(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Type = reader["Type"].ToString(),
                        Password = reader["Password"].ToString().Trim(),
                        ContactNo = reader["ContactNo"].ToString().Trim(),
                    };
               
                        string query = @"IF EXISTS(SELECT * FROM Users WHERE UserID = @UserId)
                        UPDATE Users 
                        SET  UserName = @UserName,
                       Name = @Name,
                        Email = @Email,
                      CPR = @CPR,
                        Address = @Address,
                        DOB = @DOB,
                        Type = @Type,
                       Password = @Password,
                       ContactNo = @ContactNo
                        WHERE UserID = @UserId
                    ELSE
                        INSERT INTO Users(UserId,UserName,Name,Email,CPR,Address,DOB,Type,Password,ContactNo) VALUES(@UserId,@UserName,@Name, @Email,@CPR,@Address,@DOB,@Type,@Password,@ContactNo);";
                        using (SqlConnection connection1 = new SqlConnection(strcon))
                        using (SqlCommand cmd = new SqlCommand(query, connection1))
                        {
                            cmd.Parameters.AddWithValue("@UserId", user1.UserID);
                            cmd.Parameters.AddWithValue("@UserName",user1.UserName);
                            cmd.Parameters.AddWithValue("@Name", user1.Name);
                            cmd.Parameters.AddWithValue("@Email", user1.Email);
                            cmd.Parameters.AddWithValue("@CPR", user1.CPR);
                            cmd.Parameters.AddWithValue("@Address", user1.Address);
                            cmd.Parameters.AddWithValue("@DOB", user1.DOB);
                            cmd.Parameters.AddWithValue("@Type", user1.Type);
                            cmd.Parameters.AddWithValue("@Password", user1.Password);
                            cmd.Parameters.AddWithValue("@ContactNo",user1.ContactNo);
                            // open connection, execute query, close connection
                            connection1.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            connection1.Close();
                        }
                }

                connection.Close();
                MessageBox.Show("the data inserted and updated succesfully" );
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
                connection.Close();
            }
        }

          
        public bool CheckIfUserExists(int userID)
        {
            try
            {
                var user = ty.Users.Where(m => m.UserID == userID).Single();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;

        }



    }
}
