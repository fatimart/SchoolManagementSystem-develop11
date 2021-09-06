using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class StudentListViewModel : ViewModelBase
    {
        public User user;

        private SchoolMSEntities1 ty = new SchoolMSEntities1();
        public ObservableCollection<User> AllUsers { get; private set; }
        public ObservableCollection<User> users { get; private set; }

        private int _UserID;

        public int UserID
        {
            get { return _UserID; }
            set
            {
                
                    _UserID = value;
                    OnPropertyChanged("UserID");
                
            }
        }
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set
            {
               _UserName = value;
                    OnPropertyChanged("UserName");
                
            }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
               
                    _Name = value;
                    OnPropertyChanged("Name");
                
            }
        }
        private string _Email;

        public string Email
        {
            get { return _Email; }
            set
            {
              _Email = value;
                    OnPropertyChanged("Email");
              
            }
        }
        private decimal _CPR;

        public decimal CPR
        {
            get { return _CPR; }
            set
            {
               _CPR = value;
                    OnPropertyChanged("CPR");
                
            }
        }

        private string _Address;

        public string Address
        {
            get { return _Address; }
            set
            {
              _Address = value;
                    OnPropertyChanged("Address");
                
            }
        }

        private string _Type;

        public string Type
        {
            get { return _Type; }
            set
            {
             _Type = value;
                    OnPropertyChanged("Type");
                
            }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged("Password");

            }
        }

        private string _ContactNo;

        public string ContactNo
        {
            get { return _ContactNo; }
            set
            {
                _ContactNo = value;
                    OnPropertyChanged("ContactNo");
                
            }
        }

        private DateTime _DOB;
        public DateTime DOB
        {
            get { return _DOB; }
            set
            {
               _DOB = value;
                    OnPropertyChanged("DOB");
                
            }
        }



        public StudentListViewModel ()
        {
            AllUsers = GetAll();
            

        }


        public List<User> GetAll1 ()
        {
            return ty.Users.Where(m => m.Type == "Student").ToList();


        }

        public ObservableCollection<User> GetAll ()
        {
            AllUsers = new ObservableCollection<User>();
            GetAll1().ForEach(data => AllUsers.Add(new User()
            {
                UserID = Convert.ToInt32(data.UserID),
                UserName = data.UserName,
                Name = data.Name,
                Email = data.Email,
                CPR = Convert.ToDecimal(data.CPR),
                Address = data.Address,
                DOB = Convert.ToDateTime(data.DOB),
                Type = data.Type,
                Password = data.Password,
                ContactNo = data.ContactNo

            }));

            return AllUsers;
        }

        

        public void InsertUser ( string username, string name, string email, decimal cpr, string address, DateTime dob, string password, string contactNo )
        {
            try
            {
                User user1 = new User();

                user1.UserName = username;
                user1.Name = name;
                user1.Email = email;
                user1.CPR = cpr;
                user1.Address = address;
                user1.DOB = dob;
                user1.Type = "Student";
                user1.Password = password;
                user1.ContactNo = contactNo;

                if (user1.UserID <= 0)
                {
                    ty.Users.Add(user1);
                    ty.SaveChanges();
                    MessageBox.Show("New Student successfully saved.");

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                GetAll();
                ResetData();
            }

        }

        public void UpdateUser ( int userID, string username, string name, string email, decimal cpr, string address, DateTime dob, string password, string contactNo )
        {
            if (CheckIfUserExists(userID))
            {
                try
                {


                    User updateUser = (from m in ty.Users
                                       where m.UserID == userID
                                       select m).Single();

                    updateUser.Name = name;
                    updateUser.UserName = username;
                    updateUser.Email = email;
                    updateUser.CPR = cpr;
                    updateUser.Address = address;
                    updateUser.DOB = dob;
                    updateUser.Type = "Student";
                    updateUser.Password = password;
                    updateUser.ContactNo = contactNo;

                    ty.SaveChanges();
                    MessageBox.Show("Student updated.");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    GetAll();
                    ResetData();
                }
            }
            else
            {
                MessageBox.Show("Invalid user Id");

            }
        }

        public void DeleteUser ( int userID )
        {
            if (MessageBox.Show("Confirm delete of this record?", "Student", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                if (CheckIfUserExists(userID))
                {
                    try

                    {

                        var deleteUser = ty.Users.Where(m => m.UserID == userID).Single();
                        ty.Users.Remove(deleteUser);
                        ty.SaveChanges();
                        MessageBox.Show("Record successfully deleted.");

                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        GetAll();
                        ResetData();
                    }

                }
            }

            else
            {
                MessageBox.Show("Invalid user Id");

            }
        }

        public bool CheckIfUserExists ( int userID )
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

        public void ResetData ()
        {
            User user1 = new User();

            user1.Name = string.Empty;
            user1.UserID = 0;
            user1.UserName = string.Empty;
            user1.Email = string.Empty;
            user1.CPR = 0;
            user1.Address = string.Empty;
            user1.Type = "Student";
            user1.Password = string.Empty;
            user1.ContactNo = string.Empty;
        }


    }


}
