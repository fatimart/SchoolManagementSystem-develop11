using SchoolManagementSystem.Models;
using SchoolManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class CourseViewModel : ViewModelBase 
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
        public Course course;

        private ObservableCollection<Course> _courseRecords;

        public  ObservableCollection<Course> AllCourses
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

        public int CourseID
        {
            get { return course.CourseID; }
            set { course.CourseID = value; }
        } 

        public string CourseName
        {
            get { return course.CourseName; }
            set
            {
                if (course.CourseName != value)
                {
                    course.CourseName = value;
                    OnPropertyChanged("CourseName");
                }
            }
        }
        public string CourseCode
        {
            get { return course.CourseCode; }
            set
            {
                if (course.CourseCode != value)
                {
                    course.CourseCode = value;
                    OnPropertyChanged("CourseCode");
                }
            }
        }

        public string Description
        {
            get { return course.Description; }
            set
            {
                if (course.Description != value)
                {
                    course.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        public DateTime ExamDate
        {
            get { return course.ExamDate; }
            set
            {
                if (course.ExamDate != value)
                {
                    course.ExamDate = value;
                    OnPropertyChanged("ExamDate");
                }
            }
        }

      /**  public int SectionID
        {
            get { return course.SectionID; }
            set
            {
                if (course.SectionID != value)
                {
                    course.SectionID = value;
                    OnPropertyChanged("SectionID");
                }
            }
        }**/
       public void AddCourse( string courseName, string courseCode, string description, DateTime examDate)
        {

            Course course = new Course();
            course.CourseName = courseName;
            course.CourseCode = courseCode;
            course.Description = description;
            course.ExamDate = examDate;
            //course.SectionID = sectionID;

            ty.Courses.Add(course);
            ty.SaveChanges();
   
        }
   

        public void UpdateCourse(int courseID,string courseName, string courseCode, string description, DateTime examDate)
        {
     
            Course updateCourse = (from m in ty.Courses where m.CourseID == courseID select m).Single();
            updateCourse.CourseName = courseName;
            updateCourse.CourseCode = courseCode;
            updateCourse.Description = description;
            updateCourse.ExamDate = examDate;
            //updateCourse.SectionID = sectionID;
            ty.SaveChanges();

        }

        public void DeleteCourse(int courseID)
        {

            var deleteCourse = ty.Courses.Where(m => m.CourseID == courseID).Single();
            ty.Courses.Remove(deleteCourse);
            ty.SaveChanges();

        }

       public bool CheckCourseID(int courseID)
        {
            try
            {
               var CourseID= ty.Courses.Where(m => m.CourseID == courseID).SingleOrDefault();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;

        }


        //MARK: Check by Course Code
        public bool CheckCourseCode ( string courseCode )
        {
            try
            {
                var CourseCode1 = ty.Courses.Where(m => m.CourseCode == courseCode).Single();
                //if (CourseCode1 != null)
                return true;
               // else { return false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("check if code" + ex.Message);
            }
           return false;

        }

        public void try2file ( string courseName, string courseCode, string description, DateTime examDate )
        {

            if (CheckCourseCode(courseCode))
            {
                Course updateCourse = (from m in ty.Courses where m.CourseCode == courseCode select m).Single();
                updateCourse.CourseName = courseName;
                updateCourse.CourseCode = courseCode;
                updateCourse.Description = description;
                updateCourse.ExamDate = examDate;
                ty.SaveChanges();
                MessageBox.Show("Courses have been updated");


            }
        }

        public void UpdateCourse1 ( string courseName, string courseCode, string description, DateTime examDate )
        {


            if (ty.Courses.Any(o => o.CourseCode == courseCode))
            {
                Course updateCourse = (from m in ty.Courses where m.CourseCode == courseCode select m).Single();
                updateCourse.CourseName = courseName;
                updateCourse.CourseCode = courseCode;
                updateCourse.Description = description;
                updateCourse.ExamDate = examDate;
                ty.SaveChanges();
                MessageBox.Show("Courses have been updated");
            }
            else
            {
                AddCourse(courseName, courseCode, description, examDate);
                MessageBox.Show("Courses have been inserted");

            }
        }


        public void Clear()
        {
            CourseListScreen course = new CourseListScreen();
            course.courseNameTextBox.Text = course.courseCodeTextBox.Text = course.descriptionTextBox.Text = course.sectionIDTextBox.Text = "";

        }

        public Course Get ( int id )
        {
            return ty.Courses.Find(id);
        }

        public List<Course> GetAll1 ()
        {
            return ty.Courses.ToList();
        }

        public CourseViewModel()
        {
            GetAll();
        }

        public void GetAll ()
        {
            AllCourses = new ObservableCollection<Course>();
            GetAll1().ForEach(data => AllCourses.Add(new Course()
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
