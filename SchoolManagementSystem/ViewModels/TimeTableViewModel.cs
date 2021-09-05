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

        public int UserID
       {
           get { return timeTable.UserID; }
           set
           {
               if (timeTable.UserID != value)
               {
                   timeTable.UserID = value;
                   OnPropertyChanged("UserID");
               }
           }
       }

        public int CourseID
        {
            get { return timeTable.CourseID; }
            set
            {
                if (timeTable.CourseID != value)
                {
                    timeTable.CourseID = value;
                    OnPropertyChanged("CourseID");
                }
            }
        }
       
        public string RoomNo
        {
            get { return timeTable.RoomNo; }
            set
            {
                if (timeTable.RoomNo != value)
                {
                    timeTable.RoomNo = value;
                    OnPropertyChanged("RoomID");
                }
            }
        }
        public int Year
        {
            get { return timeTable.Year; }
            set
            {
                if (timeTable.Year != value)
                {
                    timeTable.Year = value;
                    OnPropertyChanged("Year");
                }
            }
        }
        public int TimeTableID
        {
            get { return timeTable.TimeTableID; }
            set
            {
                if (timeTable.TimeTableID != value)
                {
                    timeTable.TimeTableID = value;
                    OnPropertyChanged("TimeTableID");
                }
            }
        }
        public string TeacherName
        {
            get { return timeTable.TeacherName; }
            set
            {
                if (timeTable.TeacherName != value)
                {
                    timeTable.TeacherName = value;
                    OnPropertyChanged("TeacherName");
                }
            }
        }

        public string CourseName
        {
            get { return timeTable.CourseName; }
            set
            {
                if (timeTable.CourseName != value)
                {
                    timeTable.CourseName = value;
                    OnPropertyChanged("CourseName");
                }
            }
        }

        public string CourseCode
        {
            get { return timeTable.CourseCode; }
            set
            {
                if (timeTable.CourseCode != value)
                {
                    timeTable.CourseCode = value;
                    OnPropertyChanged("CourseCode");
                }
            }
        }

        public string Time
        {
            get { return timeTable.Time; }
            set
            {
                if (timeTable.Time != value)
                {
                    timeTable.Time = value;
                    OnPropertyChanged("Time");
                }
            }
        }

        public int SectionNo
        {
            get { return timeTable.SectionNo; }
            set
            {
                if (timeTable.SectionNo != value)
                {
                    timeTable.SectionNo = value;
                    OnPropertyChanged("SectionNo");
                }
            }
        }

        public DateTime Examdate
        {
            get { return timeTable.Examdate; }
            set
            {
                if (timeTable.Examdate != value)
                {
                    timeTable.Examdate = value;
                    OnPropertyChanged("Examdate");
                }
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
