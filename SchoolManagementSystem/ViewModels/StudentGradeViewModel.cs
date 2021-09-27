using SchoolManagementSystem.ModelEntity;
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


    }
}
