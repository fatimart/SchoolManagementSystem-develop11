using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class StudentGradeViewModel : ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
        public StudentGrade SGrades ;



        public int ID
    {
            get { return SGrades.ID; }
            set
            {
                if (SGrades.ID != value)
                {
                    SGrades.ID = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        public int CourseID
        {
            get { return SGrades.StudentID; }
            set
            {
                if (SGrades.CourseID != value)
                {
                    SGrades.CourseID = value;
                    OnPropertyChanged("CourseID");
                }
            }
        }

        public int StudentID
        {
            get { return SGrades.StudentID; }
            set
            {
                if (SGrades.StudentID != value)
                {
                    SGrades.StudentID = value;
                    OnPropertyChanged("StudentID");
                }
            }
        }

        public int Score
        {
            get { return SGrades.Score; }
            set
            {
                if (SGrades.Score != value)
                {
                    SGrades.Score = value;
                    OnPropertyChanged("Score");
                }
            }
        }

        public int Attendance
        {
            get { return SGrades.Attendance; }
            set
            {
                if (SGrades.Attendance != value)
                {
                    SGrades.Attendance = value;
                    OnPropertyChanged("Attendance");
                }
            }
        }
        public bool Done
        {
            get { return SGrades.Done; }
            set
            {
                if (SGrades.Done != value)
                {
                    SGrades.Done = value;
                    OnPropertyChanged("Done");
                }
            }
        }
        public int year
        {
            get { return SGrades.year; }
            set
            {
                if (SGrades.year != value)
                {
                    SGrades.year = value;
                    OnPropertyChanged("yearID");
                }
            }
        }

        public void AddStudentGrade( int CourseID, int StudentID, int Score, int Attendance, bool Done, int yearID)
        {

            StudentGrade SGrade1 = new StudentGrade();
            SGrade1.CourseID = CourseID;
            SGrade1.StudentID = StudentID;
            SGrade1.Score = Score;
            SGrade1.Attendance = Attendance;
            SGrade1.Done = Done;
            SGrade1.year = yearID;

            ty.StudentGrades.Add(SGrade1);
            ty.SaveChanges();

        }


        public void UpdateStudentGrade(int ID, int CourseID, int StudentID, int Score, int Attendance, bool Done, int yearID)
        {

            StudentGrade updateSGrade = (from m in ty.StudentGrades where m.ID == ID select m).Single();
            updateSGrade.ID = ID;
            updateSGrade.CourseID = CourseID;
            updateSGrade.StudentID = StudentID;
            updateSGrade.Score = Score;
            updateSGrade.Attendance = Attendance;
            updateSGrade.Done = Done;
            updateSGrade.year = yearID;
            ty.SaveChanges();

        }
        public void UpdateStudentGrade1(int ID, int StudentID, int Score, int Attendance, bool Done)
        {

            StudentGrade updateSGrade = (from m in ty.StudentGrades where m.ID == ID select m).Single();
            updateSGrade.Score = Score;
            updateSGrade.Attendance = Attendance;
            updateSGrade.Done = Done;
            ty.SaveChanges();

        }
        public void deleteStudentGrade(int ID)
        {

            var deleteStudentGrade = ty.StudentGrades.Where(m => m.ID == ID).Single();
            ty.StudentGrades.Remove(deleteStudentGrade);
            ty.SaveChanges();

        }
        public bool CheckSGID(int ID)
        {
            try
            {
                var SGID = ty.StudentGrades.Where(m => m.ID == ID).Single();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;

        }
    


    }
}
