using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class EditProfileViewModel : ViewModelBase
    {

        public User user;
        private SchoolMSEntities1 ty = new SchoolMSEntities1();

        public EditProfileViewModel ()
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                SqlCommand command = new SqlCommand(
                     "select * from Users where UserID='" + UserViewModel.userSession.UserID + "'", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user = new User
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            UserName = reader["UserName"].ToString(),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            CPR = Convert.ToInt32(reader["CPR"]),
                            Address = reader["Address"].ToString(),
                            DOB = Convert.ToDateTime(reader["DOB"]),
                            Type = reader["Type"].ToString(),
                            Password = reader["Password"].ToString(),
                            ContactNo = reader["ContactNo"].ToString(),
                        };
                    }
                }
                //MessageBox.Show("edit" + UserViewModel.userSession.UserID.ToString());
                reader.Close();
            }

        }

        public void UpdateUser ( int userID, string name, string email, decimal cpr, string address, DateTime dob, string password, string contactNo )
        {

            try
                {
                    User updateUser = (from m in ty.Users
                                       where m.UserID == userID
                                       select m).Single();

                    updateUser.Name = name;
                    updateUser.Email = email;
                    updateUser.CPR = cpr;
                    updateUser.Address = address;
                    updateUser.DOB = dob;
                    updateUser.Password = password;
                    updateUser.ContactNo = contactNo;

                    ty.SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

    }
}