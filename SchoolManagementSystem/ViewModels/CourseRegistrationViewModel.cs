using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.ViewModels
{
    public class CourseRegistrationViewModel
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
        public Course course;
        private CourseViewModel courseRecord;

        public CourseRegistrationViewModel()
        {
            GetAll();

        }



        public void GetAll ()
        {
            courseRecord.AllCourses = new ObservableCollection<Course>();
            courseRecord.GetAll1().ForEach(data => courseRecord.AllCourses.Add(new Course()
            {
                CourseID = Convert.ToInt32(data.CourseID),
                CourseName = data.CourseName,
                CourseCode = data.CourseCode,
                Description = data.Description,
                ExamDate = Convert.ToDateTime(data.ExamDate),
                //SectionID = data.SectionID

        }));
            //return AllUsers;

        }

    }
}
