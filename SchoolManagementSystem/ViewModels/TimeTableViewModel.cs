using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementSystem.ViewModels
{
    class TimeTableViewModel : ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
        public TimeTable timeTable;

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

        private int _UserID;
        public int UserID
       {
           get { return _UserID; }
           set
           {
               _UserID = value;
                   OnPropertyChanged("UserID");
               
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

        private string _RoomNo;
        public string RoomNo
        {
            get { return _RoomNo; }
            set
            {
                
                    _RoomNo = value;
                    OnPropertyChanged("RoomID");
                
            }
        }

        private int _Year;

        public int Year
        {
            get { return _Year; }
            set
            {
               _Year = value;
                    OnPropertyChanged("Year");
                
            }
        }

        private int _TimeTableID;
        public int TimeTableID
        {
            get { return _TimeTableID; }
            set
            {
                _TimeTableID = value;
                    OnPropertyChanged("TimeTableID");
                
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

        private string _Time;
        public string Time
        {
            get { return _Time; }
            set
            {
                _Time = value;
                    OnPropertyChanged("Time");
                
            }
        }

        private int _SectionNo;
        public int SectionNo
        {
            get { return _SectionNo; }
            set
            {
                _SectionNo = value;
                OnPropertyChanged("SectionNo");
                
            }
        }

        private DateTime _Examdate;
        public DateTime Examdate
        {
            get { return _Examdate; }
            set
            {
                _Examdate = value;
                    OnPropertyChanged("Examdate");
                
            }
        }

        //MARK: used in course registration page
        public bool checkifRecordTableExsist ( string courseCode, int userID )
        {
            if (!ty.TimeTables.Any(o => o.CourseCode == courseCode && o.UserID == userID))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void DeleteTimeTable (int userID, string courseCode )
        {
            var deleteTimeTables = ty.TimeTables.Where(m => m.UserID == userID).Where(m => m.CourseCode == courseCode).Single();
                ty.TimeTables.Remove(deleteTimeTables);
                ty.SaveChanges();
           
        }

        public void InsertTimeTable ( int userID, int CourseID, string RoomID, int YearID, string TeacherName, string coursename, string time, string courseCode, int SectionNo, DateTime Examdate)
        {
            if (checkifRecordTableExsist(courseCode, userID))
            {
                try


                {
                    TimeTable TTable = new TimeTable();
                    TTable.UserID = userID;
                    TTable.CourseID = CourseID;
                    TTable.RoomNo = RoomID;
                    TTable.Year = YearID;
                    TTable.TeacherName = TeacherName;
                    TTable.CourseName = coursename;
                    TTable.Time = time;
                    TTable.CourseCode = courseCode;
                    TTable.SectionNo = SectionNo;
                    TTable.Examdate = Examdate;

                    ty.TimeTables.Add(TTable);
                    ty.SaveChanges();
                    MessageBox.Show("Course Added to the schedule.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " +ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Course registred already!!!!");

            }

        }

        public List<TimeTable> GetAll1 ()
        {
            return ty.TimeTables.ToList();
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

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(param => DeleteTimeTable((int)param), null);

                return _deleteCommand;
            }
        }



        //MARK: Need to edit based on the updtaed database
        public void AddTimeTable(int userID, int CourseID, string RoomID, int YearID,  string TeacherName, string coursename)
        {

            try
            {
                TimeTable TTable = new TimeTable();
                TTable.UserID = userID;
                TTable.CourseID = CourseID;
                TTable.RoomNo = RoomID;
                TTable.Year = YearID;
                TTable.TeacherName = TeacherName;
                TTable.CourseName = coursename;


                ty.TimeTables.Add(TTable);
                ty.SaveChanges();
                MessageBox.Show("Course Added to the schedule.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
           
        }

        //MARK: Need to edit based on the updtaed database
        public void UpdateTimeTable(int CourseID, string RoomID, int YearID, int TimeTableID, string TeacherName)
        {

            TimeTable updaTimeTables = (from m in ty.TimeTables where m.TimeTableID == TimeTableID select m).Single();
            updaTimeTables.CourseID = CourseID;
            updaTimeTables.RoomNo = RoomID;
            updaTimeTables.Year = YearID;
            updaTimeTables.TimeTableID = TimeTableID;
            updaTimeTables.TeacherName = TeacherName;
            ty.SaveChanges();

        }

        public void DeleteTimeTable(int TimeTableID)
        {
            if (MessageBox.Show("Confirm delete of this record?", "Course", MessageBoxButton.YesNo)
                 == MessageBoxResult.Yes)
            {
                try

                {

                    var deleteTimeTables = ty.TimeTables.Where(m => m.TimeTableID == TimeTableID).Single();
                    ty.TimeTables.Remove(deleteTimeTables);
                    ty.SaveChanges();
                    MessageBox.Show("Record successfully deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
        public bool CheckTimeTableID(int TimeTableID)
        {
            try
            {
                var TTableID = ty.TimeTables.Where(m => m.TimeTableID == TimeTableID).Single();
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
