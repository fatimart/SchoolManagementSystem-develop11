
/**using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace SchoolManagementSystem.ViewModels
{
     class CourseRegistrationViewModel : ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();

        private ObservableCollection<TimeTable> _courseRecords;
        public ObservableCollection<TimeTable> AllCourses
        {
            get
            {
                return _courseRecords;
            }
            set
            {
                _courseRecords = value;
                OnPropertyChanged("AllCourses");
            }
        }

        public CourseRegistrationViewModel ()
        {
            GetAll();
        }

   

        public void GetAll ()
        {
            AllCourses = new ObservableCollection<TimeTable>();
            GetAll1().ForEach(data => AllCourses.Add(new TimeTable()
            {

                UserID = Convert.ToInt32(data.UserID),
                CourseID = Convert.ToInt32(data.CourseID),
                RoomNo = data.RoomNo,
                Year = data.Year,
                TeacherName = data.TeacherName,
                CourseName = data.CourseName,
                Time = data.Time,
                CourseCode = data.CourseCode,
                SectionNo = data.SectionNo,
                Examdate = Convert.ToDateTime(data.Examdate)

            }));

        }

        public List<TimeTable> GetAll1 ()
        {
            return ty.TimeTables.Where(m => m.UserID == UserViewModel.userSession.UserID).ToList();
        }
        
    }
}

**/