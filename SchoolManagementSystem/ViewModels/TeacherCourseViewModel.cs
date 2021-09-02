using SchoolManagementSystem.Models;
using SchoolManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class TeacherCourseViewModel :ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
        public TeacherCours TCourse;
        public TeacherCourseView TCourse1;

        public int TID
        {
            get { return TCourse.TID; }
            set
            {
                if (TCourse.TID != value)
                {
                    TCourse.TID = value;
                    OnPropertyChanged("TID");
                }
            }
        }
        public string TeacherName
        {
            get { return TCourse.TeacherName; }
            set
            {
                if (TCourse.TeacherName != value)
                {
                    TCourse.TeacherName = value;
                    OnPropertyChanged("TeacherName");
                }
            }
        }
        public string CourseCode
        {
            get { return TCourse.CourseCode; }
            set
            {
                if (TCourse.CourseCode != value)
                {
                    TCourse.CourseCode = value;
                    OnPropertyChanged("CourseCode");
                }
            }
        }
        public void AddTeacherCourse(string teacherName,string courseCode)
        {
            try


            {
               TeacherCours TCourse1= new TeacherCours();
                TCourse1.TeacherName = teacherName;
                TCourse1.CourseCode = courseCode;
             


                ty.TeacherCourses.Add(TCourse1);
                ty.SaveChanges();
                MessageBox.Show("Course Added to the schedule.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }


        }


        public void UpdateTeacherCourse(int tid, string teacherName, string courseCode)
        {

            TeacherCours UpdateTeacherCourse1 = (from m in ty.TeacherCourses where m.TID == tid select m).Single();
            UpdateTeacherCourse1.TID = tid;
            UpdateTeacherCourse1.TeacherName = teacherName;
            UpdateTeacherCourse1.CourseCode = courseCode;
    
            ty.SaveChanges();

        }

        public void DeleteTeacherCourse(int Tid)
        {

            var DeleteTeacherCourses = ty.TeacherCourses.Where(m => m.TID == Tid).Single();
            ty.TeacherCourses.Remove(DeleteTeacherCourses);
            ty.SaveChanges();

        }
        public bool CheckTeacherCourseID(int Tid)
        {
            try
            {
                var TCourse1 = ty.TeacherCourses.Where(m => m.TID == Tid).Single();
                //var TTable = ty.TimeTables.Where(m => m.UserID == UserID).Single();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;

        }
        public void FindTeacherCourse(int Tid)
        {
            try
            {

                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("select CourseCode,TeacherName from TeacherCourses where TID='" + Tid + "'", connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {
                        
                         TCourse1.course_combo_box.SelectedValue = dt.Rows[0]["CourseCode"].ToString();
                        TCourse1.teacher_combo_box.SelectedValue = dt.Rows[0]["TeacherName"].ToString();
                  
                    }
                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }




    }
}
