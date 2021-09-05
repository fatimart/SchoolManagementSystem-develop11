using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    public class CourseRegistrationViewModel
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();

        private TimeTableViewModel courseRecord;

        public CourseRegistrationViewModel()
        {
            //GetAll();


        }

        public void GetAll ()
        {
            courseRecord.AllCourses = new ObservableCollection<TimeTable>();
            courseRecord.GetAll1().ForEach(data => courseRecord.AllCourses.Add(new TimeTable()
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

            MessageBox.Show("show");

        }


    }
}
