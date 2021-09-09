/**using Microsoft.Win32;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Views;
using SchoolManagementSystem.Views.AdminViews;
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

namespace SchoolManagementSystem.ViewModels
{
    class UsersUploadViewModel : ViewModelBase
    {
        UsersUpload UUpload = new UsersUpload();
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
        public void FillDataGrid()
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "select File";
            fdlg.FileName = UUpload.txtFilePath.Text;
            fdlg.DefaultExt = ".xlsx";
            fdlg.Filter = "(.xlsx)|*.xlsx";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == true)
            {
                UUpload.txtFilePath.Text = fdlg.FileName;
            }

            string commandText = "SELECT * FROM [Sheet1$]";
            string oledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source=" + fdlg.FileName + ";" +
            "Extended Properties=\"Excel 12.0;HDR=YES\";";
            OleDbConnection connection = new OleDbConnection(oledbConnectString);
            OleDbCommand command = new OleDbCommand(commandText, connection);
            try
            {
                connection.Open();
                DataTable dt = new DataTable();
                OleDbDataAdapter Adpt = new OleDbDataAdapter(commandText, connection);
                Adpt.Fill(dt);
                UUpload.dtGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception)
            {
            }




        }
        public void Import()
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "select File";
            fdlg.FileName = UUpload.txtFilePath.Text;
            fdlg.DefaultExt = ".xlsx";
            fdlg.Filter = "(.xlsx)|*.xlsx";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == true)
            {
                UUpload.txtFilePath.Text = fdlg.FileName;
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
                UUpload.dtGrid.ItemsSource = dt.DefaultView;
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

                    string query = @"IF EXISTS(SELECT * FROM Users WHERE UserName = @UserName)
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
                        WHERE UserName = @UserName
                    ELSE
                        INSERT INTO Users(UserName,Name,Email,CPR,Address,DOB,Type,Password,ContactNo) VALUES(@UserName,@Name, @Email,@CPR,@Address,@DOB,@Type,@Password,@ContactNo);";
                    using (SqlConnection connection1 = new SqlConnection(strcon))
                    using (SqlCommand cmd = new SqlCommand(query, connection1))
                    {
                        cmd.Parameters.AddWithValue("@UserId", user1.UserID);
                        cmd.Parameters.AddWithValue("@UserName", user1.UserName);
                        cmd.Parameters.AddWithValue("@Name", user1.Name);
                        cmd.Parameters.AddWithValue("@Email", user1.Email);
                        cmd.Parameters.AddWithValue("@CPR", user1.CPR);
                        cmd.Parameters.AddWithValue("@Address", user1.Address);
                        cmd.Parameters.AddWithValue("@DOB", user1.DOB);
                        cmd.Parameters.AddWithValue("@Type", user1.Type);
                        cmd.Parameters.AddWithValue("@Password", user1.Password);
                        cmd.Parameters.AddWithValue("@ContactNo", user1.ContactNo);
                        // open connection, execute query, close connection
                        connection1.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        connection1.Close();
                    }
                }

                connection.Close();
                MessageBox.Show("the data inserted and updated succesfully");
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
**/