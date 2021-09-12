using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class StudentGradeViewModel : ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();

        private ObservableCollection<StudentGrade> _GradeRecords;
        public ObservableCollection<StudentGrade> AllGrades
        {
            get
            {
                return _GradeRecords;
            }
            set
            {
                _GradeRecords = value;
                OnPropertyChanged("AllGrades");
            }
        }

        private int _ID;
        public int ID
        {
                get { return _ID; }
                set
                {
                    _ID = value;
                        OnPropertyChanged("ID");
                
                }
            }

        private int _CourseID;
        public int CourseID
        {
            get { return _CourseID; }
            set
            {
                _CourseID = value;
                    OnPropertyChanged("CourseID");
                }
            
        }

    private int _StudentID;
        public int StudentID
        {
            get { return _StudentID; }
            set
            {
                _StudentID = value;
                    OnPropertyChanged("StudentID");
                }
            
        }

    private int _Score;
        public int Score
        {
            get { return _Score; }
            set
            {
                _Score = value;
                    OnPropertyChanged("Score");
                }
            
        }

    private int _Attendance;
        public int Attendance
        {
            get { return _Attendance; }
            set
            {
                _Attendance = value;
                    OnPropertyChanged("Attendance");
                
            }
        }

        private bool _Done;
        public bool Done
        {
            get { return _Done; }
            set
            {
                _Done = value;
                    OnPropertyChanged("Done");
                
            }
        }

        private int _year;
        public int year
        {
            get { return _year; }
            set
            {
                _year = value;
                    OnPropertyChanged("yearID");
                
            }
        }

        private int _SectionID;
        public int SectionID
        {
            get { return _SectionID; }
            set
            {
                _SectionID = value;
                OnPropertyChanged("yearID");

            }
        }

        public List<StudentGrade> GetAll1 ()
        {
            return ty.StudentGrades.ToList();
        }

        public void GetAll ()
        {
            AllGrades = new ObservableCollection<StudentGrade>();
            GetAll1().ForEach(data => AllGrades.Add(new StudentGrade()
            {
                CourseID = data.CourseID,
                StudentID = data.StudentID,
                Score = data.Score,
                Attendance = data.Attendance,
                Done = data.Done,
                year = data.year

            }));

        }

        //MARK: DataAccess
        public void AddStudentGrade( int CourseID, int StudentID, int Score, int Attendance, bool Done, int yearID, int sectionID)
        {

            StudentGrade SGrade1 = new StudentGrade();
            SGrade1.CourseID = CourseID;
            SGrade1.StudentID = StudentID;
            SGrade1.Score = Score;
            SGrade1.Attendance = Attendance;
            SGrade1.Done = Done;
            SGrade1.year = yearID;
            SGrade1.SectionID = sectionID;
            ty.StudentGrades.Add(SGrade1);
            ty.SaveChanges();

        }


        public void UpdateStudentGrade(int ID, int CourseID, int StudentID, int Score, int Attendance, bool Done, int yearID, int sectionID)
        {

            StudentGrade updateSGrade = (from m in ty.StudentGrades where m.ID == ID select m).Single();
            updateSGrade.ID = ID;
            updateSGrade.CourseID = CourseID;
            updateSGrade.StudentID = StudentID;
            updateSGrade.Score = Score;
            updateSGrade.Attendance = Attendance;
            updateSGrade.Done = Done;
            updateSGrade.year = yearID;
            updateSGrade.SectionID = sectionID;
            ty.SaveChanges();

        }

        public void UpdateStudentGrade1 ( int ID, int StudentID, int Score, int Attendance, bool Done )
        {

            StudentGrade updateSGrade = (from m in ty.StudentGrades where m.ID == ID select m).Single();
            updateSGrade.Score = Score;
            updateSGrade.Attendance = Attendance;
            updateSGrade.Done = Done;
            ty.SaveChanges();

        }

        public void deleteStudentGrade(int UserID, int courseID, int sectionID)
        {

            var deleteStudentGrade = ty.StudentGrades.Where(m => m.SectionID == sectionID).Where(m => m.StudentID == UserID).Where(m => m.CourseID == courseID).Single();
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
