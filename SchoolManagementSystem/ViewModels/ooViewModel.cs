

 /** using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using SchoolManagementSystem.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Web.ModelBinding;
using System.Configuration;
using System.Data.SqlClient;


namespace SchoolManagementSystem.ViewModels
{

    class UserViewModel : ViewModelBase
    {
        #region Fields
        private int _userID;
        private User _user;
        private ObservableCollection<User> _users;
        private ICommand _SaveUserCommand;
        private ICommand _getUserCommand;
        private SchoolMSEntities db = new SchoolMSEntities();
        #endregion

        public User user
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }
        public ObservableCollection<User> Users
        {
            get { return _users;}
            set
            {
                _users = value;
                OnPropertyChanged("Users");
            }
        }
        public ICommand SaveUserCommand
        {
            get
            {
                if (_SaveUserCommand == null)
                {
                    this._SaveUserCommand = new RelayCommand
                        (param => SaveUser(), null);
                }
                return _SaveUserCommand;
            }
        }

        public UserViewModel ()
        {

            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                SqlCommand command = new SqlCommand(
                  "SELECT TOP 1 * FROM Users;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user = new User { Name = reader["Name"].ToString() };
                    }
                }
                reader.Close();
            }

        }

        //Whenever new item is added to the collection, am explicitly calling notify property changed  
        void Users_CollectionChanged ( object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e )
        {
            OnPropertyChanged("Users");
        }


        public ICommand GetUserCommand
        {
            get
            {
                if (_getUserCommand == null)
                {
                    _getUserCommand = new RelayCommand(
                        param => GetUser(),
                        param => UserID > 0
                    );
                }
                return _getUserCommand;
            }
        }

        public int UserID
        {
            get { return _userID; }
            set
            {
                if (value != _userID)
                {
                    _userID = value;
                    OnPropertyChanged("UserID");
                }
            }
        }

        //functions
        private void GetUser ()
        {
            var users = db.Users.ToList();
            
        }
  
        private void SaveUser ()
        {
           
            db.Users.Add(user);
            db.SaveChanges();
        }
        
    }
}
**/