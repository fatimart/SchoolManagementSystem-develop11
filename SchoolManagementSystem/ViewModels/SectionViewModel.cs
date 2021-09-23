using SchoolManagementSystem.Models;
using SchoolManagementSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<Section> _sectionRecord;
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
            var roomDetails = WebAPI.GetCall(API_URIs.sections);
            if (roomDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AllSections = roomDetails.Result.Content.ReadAsAsync<ObservableCollection<Section>>().Result;
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

            var roomDetails = WebAPI.PostCall(API_URIs.sections, newSection);
            if (roomDetails.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                ResponseMessage = newSection.SectionNum + "'s details has successfully been added!";
                MessageBox.Show("created ");
            }
            else
            {
                ResponseMessage = "Failed to update" + newSection.SectionNum + "'s details.";
            }
        }


        /// <summary>
        /// Updates Section's record
        /// </summary>
        /// <param name="sections"></param>
        public void UpdateSectionDetails ( int sectionNum, int courseID, int RoomID, string time )
        {
            Section updateSection = new Section()
            {
                SectionNum = sectionNum,
                CourseID = courseID,
                RoomID = RoomID,
                Time = time
            };


            var roomDetails = WebAPI.PutCall(API_URIs.sections + "?id=" + updateSection.SectionID, updateSection);
            if (roomDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = updateSection.SectionID + "'s details has successfully been updated!";
            }
            else
            {
                ResponseMessage = "Failed to update" + updateSection.SectionID + "'s details.";
            }
        }

        /// <summary>
        /// Deletes Section's record
        /// </summary>
        public void DeleteSectionDetails ( int SectionID, int courseID )
        {

            Section deleteSection = new Section()
            {
                SectionID = SectionID,
                CourseID = courseID

            };

            var roomDetails = WebAPI.DeleteCall(API_URIs.sections + "?id=" + deleteSection.SectionID);
            if (roomDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = deleteSection.SectionID + "'s details has successfully been deleted!";
            }
            else
            {
                ResponseMessage = "Failed to delete" + deleteSection.SectionID + "'s details.";
            }
        }
        #endregion




    }
}
