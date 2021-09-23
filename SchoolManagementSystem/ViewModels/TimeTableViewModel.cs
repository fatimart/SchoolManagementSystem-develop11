using SchoolManagementSystem.Models;
using SchoolManagementSystem.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class TimeTableViewModel : ViewModelBase
    {
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

        private int _SectionID;
        public int SectionID
        {
            get { return _SectionID; }
            set
            {
                _SectionID = value;
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

        private string _responseMessage = "";
        public string ResponseMessage
        {
            get { return _responseMessage; }
            set
            {
                _responseMessage = value;
                OnPropertyChanged("ResponseMessage");
            }
        }


        #region CRUD

        /// <summary>
        /// Fetches Room details
        /// </summary>
        public void GetTimeTableetails ()

        {
            var tableDetails = WebAPI.GetCall(API_URIs.timetables);
            if (tableDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AllCourses = tableDetails.Result.Content.ReadAsAsync<ObservableCollection<TimeTable>>().Result;
            }
        }

        /// <summary>
        /// Adds new Room
        /// </summary>
        public void CreateNewTimeTable ( int userID, int CourseID, string RoomID, int YearID, string TeacherName,
            string coursename, string time, string courseCode, int SectionNo, DateTime Examdate, int sectionID )
        {
            TimeTable TTable = new TimeTable()
            {
                UserID = userID,
                CourseID = CourseID,
                RoomNo = RoomID,
                Year = YearID,
                TeacherName = TeacherName,
                CourseName = coursename,
                Time = time,
                CourseCode = courseCode,
                SectionNo = SectionNo,
                Examdate = Examdate,
                SectionID = sectionID
            };

            var tableDetails = WebAPI.PostCall(API_URIs.timetables, TTable);
            if (tableDetails.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                ResponseMessage = TTable.TimeTableID + "'s details has successfully been added!";
                MessageBox.Show("created ");
            }
            else
            {
                ResponseMessage = "Failed to update" + TTable.TimeTableID + "'s details.";
            }
        }


        /// <summary>
        /// Deletes TimeTable's record
        /// </summary>
        public void DeleteTimeTableDetails ( int TimeTableID )
        {

            TimeTable deleteTimeTable = new TimeTable()
            {
                TimeTableID = TimeTableID
            };

            var tableDetails = WebAPI.DeleteCall(API_URIs.timetables + "?id=" + deleteTimeTable.TimeTableID);
            if (tableDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = deleteTimeTable.TimeTableID + "'s details has successfully been deleted!";
            }
            else
            {
                ResponseMessage = "Failed to delete" + deleteTimeTable.TimeTableID + "'s details.";
            }
        }
        #endregion

    }
}
