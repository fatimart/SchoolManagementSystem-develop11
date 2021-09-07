using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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

        private int _CourseID;
        public int CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value; }
        }

        private string _CourseName;
        public string CourseName
        {
            get { return _CourseName; }
            set
            {
               
                    _CourseName = value;
                    OnPropertyChanged("CourseName");
                
            }
        }

        private string _CourseCode;
        public string CourseCode
        {
            get { return _CourseCode; }
            set
            {
                _CourseCode = value;
                    OnPropertyChanged("CourseCode");
                
            }
        }


        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {

                _Description = value;
                    OnPropertyChanged("Description");
                
            }
        }

        private DateTime _ExamDate;
        public DateTime ExamDate
        {
            get { return _ExamDate; }
            set
            {
                _ExamDate = value;
                    OnPropertyChanged("ExamDate");
                
            }
        }


       public void AddCourse( string courseName, string courseCode, string description, DateTime examDate)
        {
           
                Course course1 = new Course();
                course1.CourseName = courseName;
                course1.CourseCode = courseCode;
                course1.Description = description;
                course1.ExamDate = examDate;

                if (course1.CourseID <= 0)
                {
                    ty.Courses.Add(course1);
                    ty.SaveChanges();
                    //MessageBox.Show("New Course successfully Added.");

                }
            
            
        }
   

        public void UpdateCourse(int courseID,string courseName, string courseCode, string description, DateTime examDate)
        {
           

                    Course updateCourse = (from m in ty.Courses where m.CourseCode == courseCode select m).Single();
                    updateCourse.CourseName = courseName;
                    updateCourse.CourseCode = courseCode;
                    updateCourse.Description = description;
                    updateCourse.ExamDate = examDate;

                    ty.SaveChanges();
                    MessageBox.Show("Course updated.");

        }

        //MARK: Update by course code 
        public void UpdateCourseV2 ( string courseName, string courseCode, string description, DateTime examDate )
        {

            Course updateCourse = (from m in ty.Courses where m.CourseCode == courseCode select m).Single();
            updateCourse.CourseName = courseName;
            updateCourse.CourseCode = courseCode;
            updateCourse.Description = description;
            updateCourse.ExamDate = examDate;
            ty.SaveChanges();

        }




        public void DeleteCourse ( string courseCode )
        {
            var deleteCourse = ty.Courses.Where(m => m.CourseCode == courseCode).Single();
            ty.Courses.Remove(deleteCourse);
            ty.SaveChanges();
            MessageBox.Show("Record successfully deleted.");
            
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

        public bool checkCourseCode(string CourseCode)
        {
            if (ty.Courses.Any(o => o.CourseCode == CourseCode))
            { return true; }
            else
            {
                return false;
            }
        }

        //MARK: update and insert when uploading new excel sheet
        public void UpdateCourse1 ( string courseName, string courseCode, string description, DateTime examDate )
        {

            if (ty.Courses.Any(o => o.CourseCode == courseCode))
            {
                UpdateCourseV2(courseName, courseCode, description, examDate);
            }

            else
            {
                AddCourse(courseName, courseCode, description, examDate);
            }
        }

        public void ResetData ()
        {
            Course course1 = new Course();

            course1.CourseID = 0;
            course1.CourseCode = string.Empty;
            course1.CourseName = string.Empty;
            course1.Description = string.Empty;
           
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

            }));

        }

        //MARK: Get all courses details for time table

        public List<Course> getCourseDetails ()
        {
            List<Course> courses = new List<Course>();
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                SqlCommand command = new SqlCommand(
                  "SELECT * FROM Course;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseID = Convert.ToInt32(reader["CourseID"]),
                            CourseName = reader["CourseName"].ToString(),
                            CourseCode = reader["CourseCode"].ToString(),
                            Description = reader["Description"].ToString(),
                            ExamDate = Convert.ToDateTime(reader["ExamDate"])
                        });
                    }
                }
                reader.Close();
            }
            return courses;

        }
    }
}
