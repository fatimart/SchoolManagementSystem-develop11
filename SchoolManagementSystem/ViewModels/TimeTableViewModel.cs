using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
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
        public int SectionID

        {
            get { return timeTable.SectionID; }
            set
            {
                if (timeTable.SectionID != value)
                {
                    timeTable.SectionID = value;
                    OnPropertyChanged("SectionID");
                }
            }
        }
        public int RoomID
        {
            get { return timeTable.RoomID; }
            set
            {
                if (timeTable.RoomID != value)
                {
                    timeTable.RoomID = value;
                    OnPropertyChanged("RoomID");
                }
            }
        }
        public int YearID
        {
            get { return timeTable.YearID; }
            set
            {
                if (timeTable.YearID != value)
                {
                    timeTable.YearID = value;
                    OnPropertyChanged("YearID");
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


        public void AddTimeTable(int userID, int CourseID, int SectionID, int RoomID, int YearID,  string TeacherName)
        {

            TimeTable TTable = new TimeTable();
            TTable.UserID = userID;
            TTable.CourseID = CourseID;
            TTable.SectionID = SectionID;
            TTable.RoomID = RoomID;
            TTable.YearID = YearID;
            TTable.TeacherName = TeacherName;
       

            ty.TimeTables.Add(TTable);
            ty.SaveChanges();

        }


        public void UpdateTimeTable(int CourseID, int SectionID, int RoomID, int YearID, int TimeTableID, string TeacherName)
        {

            TimeTable updaTimeTables = (from m in ty.TimeTables where m.TimeTableID == TimeTableID select m).Single();
            updaTimeTables.CourseID = CourseID;
            updaTimeTables.SectionID = SectionID;
            updaTimeTables.RoomID = RoomID;
            updaTimeTables.YearID = YearID;
            updaTimeTables.TimeTableID = TimeTableID;
            updaTimeTables.TeacherName = TeacherName;
            ty.SaveChanges();

        }

        public void DeleteTimeTable(int TimeTableID)
        {

            var deleteTimeTables = ty.TimeTables.Where(m => m.TimeTableID == TimeTableID).Single();
            ty.TimeTables.Remove(deleteTimeTables);
            ty.SaveChanges();

        }
        public bool CheckTimeTableID(int TimeTableID)
        {
            try
            {
                var TTableID = ty.TimeTables.Where(m => m.TimeTableID == TimeTableID).Single();
                //var TTable = ty.TimeTables.Where(m => m.UserID == UserID).Single();

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
