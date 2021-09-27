using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace SchoolManagementSystemAPI.Models
{

    public class Teacher
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal CPR { get; set; }
        public string Address { get; set; }
        public System.DateTime DOB { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }

        public ObservableCollection<User> AllUsers { get; private set; }
        public ObservableCollection<User> users { get; private set; }

        public SchoolMSEntities ty = new SchoolMSEntities();

        public User Get ( int id )
        {
            return ty.Users.Find(id);
        }

        public User GetUser ( int id )
        {
            User user1 = new User();
            using (SchoolMSEntities entities = new SchoolMSEntities())
            {
                var user = entities.Users.FirstOrDefault(p => p.UserID == id);
                if (user != null)
                {
                     user1 = new User
                    {
                        UserID = user.UserID,
                        UserName = user.UserName,
                        Name = user.Name,
                        Email = user.Email,
                        CPR = user.CPR,
                        Address = user.Address,
                        DOB = user.DOB,
                        Type = user.Type,
                        Password = "Private Inforamtion",
                        ContactNo = user.ContactNo,
                    };
                }
                return user1;
            }
        }

        public List<User> GetAll1 ()
        {
            return ty.Users.Where(m => m.Type == "teacher").ToList();


        }

        public void GetAllTeachers ()
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

        }

        //MARK: DataAccess Functions

        public void InserTeacher ( User teacher )
        { 
            ty.Users.Add(teacher);
            ty.SaveChanges();

        }

        public void UpdateUser ( int userID, User teacher )
        {

            var result = (from m in ty.Users where m.UserID == userID select m).Single();
            if (result != null)
            {
                if (!string.IsNullOrWhiteSpace(teacher.UserID.ToString()))
                    result.Name = teacher.Name;
                    result.UserName = teacher.UserName;
                    result.Name = teacher.Name;
                    result.Email = teacher.Email;
                    result.CPR = teacher.CPR;
                    result.Address = teacher.Address;
                    result.DOB = teacher.DOB;
                    //result.Type = "teacher";
                    result.Password = teacher.Password;
                    result.ContactNo = teacher.ContactNo;

                ty.SaveChanges();

            }
        }

        public void DeleteUser ( int userID )
        {

            var deleteUser = ty.Users.Where(m => m.UserID == userID).Single();
            ty.Users.Remove(deleteUser);
            ty.SaveChanges();

        }
    }
}