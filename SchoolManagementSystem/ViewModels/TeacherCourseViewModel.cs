using SchoolManagementSystem.Models;
using SchoolManagementSystem.Views.AdminViews;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class TeacherCourseViewModel :ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();

        public TeacherCours TCourse;
        HttpClient client = new HttpClient();
        public TeacherCourseViewModel()
        {
            client.BaseAddress = new Uri("https://localhost:44361/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        
        }


        private int _TID;
        public int TID
        {
            get { return _TID; }
            set
            {
                _TID = value;
                    OnPropertyChanged("TID");
               
            }
        }

        private string _CourseID;
        public string CourseID
        {
            get { return _CourseID; }
            set
            {
                _CourseID = value;
                OnPropertyChanged("CourseCode");

            }
        }
        private string _SectionID;
        public string SectionID
        {
            get { return _SectionID; }
            set
            {
                _SectionID = value;
                OnPropertyChanged("CourseCode");

            }
        }

        private string _TeacherName;
        public string TeacherName
        {
            get { return _TeacherName; }
            set
            {
                _TeacherName = value;
                    OnPropertyChanged("TeacherName");
                
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

        private string _UserID;
        public string UserID
        {
            get { return _UserID; }
            set
            {
                _UserID = value;
                OnPropertyChanged("CourseCode");

            }
        }

        //MARK: DataAccess (Add/Edit/Delete)

        public void AddTeacherCourse(string teacherName,string courseCode, int CourseID, int sectionID, int UserID )
        {
            if (!checkifRecordTableExsist(CourseID, sectionID))
            {
                try

                {
                    TeacherCours TCourse1 = new TeacherCours();
                    TCourse1.TeacherName = teacherName;
                    TCourse1.CourseCode = courseCode;
                    TCourse1.CourseID = CourseID;
                    TCourse1.SectionID = sectionID;
                    TCourse1.UserID = UserID;

                    ty.TeacherCourses.Add(TCourse1);
                    ty.SaveChanges();
                    MessageBox.Show("Course Added to the schedule.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
            else
            {
                MessageBox.Show("Course Already registered");

            }

        }


        public void UpdateTeacherCourse(int tid, string teacherName, string courseCode, int CourseID, int sectionID, int UserID)
        {

            TeacherCours UpdateTeacherCourse1 = (from m in ty.TeacherCourses where m.TID == tid select m).Single();
            UpdateTeacherCourse1.TID = tid;
            UpdateTeacherCourse1.TeacherName = teacherName;
            UpdateTeacherCourse1.CourseCode = courseCode;
            UpdateTeacherCourse1.CourseID = CourseID;
            UpdateTeacherCourse1.SectionID = sectionID;
            UpdateTeacherCourse1.UserID = UserID;

            ty.SaveChanges();

        }

        public void DeleteTeacherCourse( int courseID, int sectionID, int userID )
        {
            if (checkifRecordTableExsist(courseID, sectionID))
            {
                var DeleteTeacherCourses = ty.TeacherCourses.Where(o => o.CourseID == courseID && o.UserID == userID && o.SectionID == sectionID).Single();
                ty.TeacherCourses.Remove(DeleteTeacherCourses);
                ty.SaveChanges();
            }
        }
        public bool CheckTeacherCourseID(int Tid)
        {
            try
            {
                var TCourse1 = ty.TeacherCourses.Where(m => m.TID == Tid).Single();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;

        }

        public bool checkifRecordTableExsist ( int courseID, int sectionID )
        {
            if (ty.TeacherCourses.Any(o => o.CourseID == courseID && o.SectionID == sectionID ))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
