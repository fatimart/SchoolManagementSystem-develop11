using Newtonsoft.Json;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Utilities;
using SchoolManagementSystem.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class SectionViewModel : ViewModelBase
    {
        public Section Section;
        public DataTable dt = new DataTable();
        private ObservableCollection<Room> _RoomBoxRecord;
        private ObservableCollection<Section> _sectionRecord;
        private ObservableCollection<Course> _CourseRecord;
        private ObservableCollection<Course> _courseBox;
        public string courseBox1;
        public string RoomIDBox;
        public ObservableCollection<Course> courseBox
        {
            get
            {
                return _courseBox;
            }
            set
            {
                _courseBox = value;
                OnPropertyChanged("courseBox");
            }
        }
        public ObservableCollection<Room> RoomBox
        {
            get
            {
                return _RoomBoxRecord;
            }
            set
            {
                _RoomBoxRecord = value;
                OnPropertyChanged("RoomBox");
            }
        }
        public ObservableCollection<Course> AllCourse
        {
            get
            {
                return _CourseRecord;
            }
            set
            {
                _CourseRecord = value;
                OnPropertyChanged("AllCourse");
            }
        }
        public ObservableCollection<Section> AllSections
        {
            get
            {
                return _sectionRecord;
            }
            set
            {
                _sectionRecord = value;
                OnPropertyChanged("AllSections");
            }
        }

        private int _SectionID;
        public int SectionID
        {
            get { return _SectionID; }
            set
            {
                _SectionID = value;
                OnPropertyChanged("SectionID");

            }
        }

        private int _SectionNum;
        public int SectionNum
        {
            get { return _SectionNum; }
            set
            {
                _SectionNum = value;
                OnPropertyChanged("SectionNum");

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

        private int _RoomID;
        public int RoomID
        {
            get { return _RoomID; }
            set
            {
                _RoomID = value;
                OnPropertyChanged("RoomID");

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

        public SectionViewModel ()
        {
            //GetAll();
        }

        #region CRUD

        /// <summary>
        /// Fetches Section details
        /// </summary>
        public void GetSectionetails ()

        {
            var roomDetails = WebAPI.GetCall(API_URIs.sections+ "/FillDataGrid");
            if (roomDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AllSections = roomDetails.Result.Content.ReadAsAsync<ObservableCollection<Section>>().Result;
            }
        }

        public void GetSectionDetails()

        {
            var Section = WebAPI.GetCall(API_URIs.sections + "/FillDataGrid");

            if (Section.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string result = Section.Result.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(result);
            
            }
        }
        //[Route("api/sections/FillCourseBox")]
        public  void GetCourseDetails()

        {
            var Course = WebAPI.GetCall(API_URIs.sections+ "/FillCourseBox");
  
            if (Course.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AllCourse = Course.Result.Content.ReadAsAsync<ObservableCollection<Course>>().Result;
            }
        }


        public void GetRoomBox()

        {
            var Room = WebAPI.GetCall(API_URIs.sections + "/FillRoomBox");

            if (Room.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RoomBox = Room.Result.Content.ReadAsAsync<ObservableCollection<Room>>().Result;
            }
        }
       
         public void getRoomIDD()
        //
        {
            // MessageBox.Show("/getcourseIDD?CCodeBox=" + SectionScreen.CourseComboBox + "");
            var Room = WebAPI.GetCall(API_URIs.sections + "/getRoomIDD?RoomNumBox=" + SectionScreen.RoomNumCBox+"");
 

            if (Room.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RoomIDBox = Room.Result.Content.ReadAsStringAsync().Result;
    
            }
        }
        public void getcourseIDD()
            //
        {
           // MessageBox.Show("/getcourseIDD?CCodeBox=" + SectionScreen.CourseComboBox + "");
            var Course = WebAPI.GetCall(API_URIs.sections + "/getcourseIDD?CCodeBox="+SectionScreen.CourseComboBox+"");
            if (Course.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                 courseBox1 = Course.Result.Content.ReadAsStringAsync().Result;
             
            }
        }
       

        /// <summary>
        /// Adds new Section
        /// </summary>
        public void CreateNewSection ( int sectionNum, int courseID, int RoomID, string time )
        {
            Section newSection = new Section()
            {
                SectionNum = sectionNum,
                CourseID = courseID,
                RoomID = RoomID,
                Time = time
            };

            var SectionDetails = WebAPI.PostCall(API_URIs.sections, newSection);
            if (SectionDetails.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                ResponseMessage = newSection.SectionNum + "'s details has successfully been added!";
                MessageBox.Show(ResponseMessage);
            }
            else
            {
                ResponseMessage = "Failed to update" + newSection.SectionNum + "'s details.";
                MessageBox.Show(ResponseMessage);
            }
        }


        /// <summary>
        /// Updates Section's record
        /// </summary>
        /// <param name="sections"></param>
        public void UpdateSectionDetails ( int sectionID,int sectionNum, int courseID, int RoomID, string time )
        {
            Section updateSection = new Section()
            {   SectionID= sectionID,
                SectionNum = sectionNum,
                CourseID = courseID,
                RoomID = RoomID,
                Time = time
            };


            var SectionDetails = WebAPI.PutCall(API_URIs.sections + "?id=" + updateSection.SectionID, updateSection);
            if (SectionDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {   
                ResponseMessage = updateSection.SectionID + "'s details has successfully been updated!";
                MessageBox.Show(ResponseMessage);
            }
            else
            {
                ResponseMessage = "Failed to update" + updateSection.SectionID + "'s details.";
                MessageBox.Show(ResponseMessage);
            }
        }

        /// <summary>
        /// Deletes Section's record
        /// </summary>
        public void DeleteSectionDetails ( int SectionID )
        {

            Section deleteSection = new Section()
            {
                SectionID = SectionID,

            };

            var SectionDetails = WebAPI.DeleteCall(API_URIs.sections + "?id=" + deleteSection.SectionID);
            if (SectionDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = deleteSection.SectionID + "'s details has successfully been deleted!";
                MessageBox.Show(ResponseMessage);
            }
            else
            {
                ResponseMessage = "Failed to delete" + deleteSection.SectionID + "'s details.";
                MessageBox.Show(ResponseMessage);
            }
        }
        #endregion




    }
}
