using SchoolManagementSystem.Models;
using SchoolManagementSystem.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class AnnoucnmentViewModel : ViewModelBase
    {
        private ObservableCollection<Announcement> _AnnoucnRecords;
        public ObservableCollection<Announcement> AllAnnounc
        {
            get
            {
                return _AnnoucnRecords;
            }
            set
            {
                _AnnoucnRecords = value;
                OnPropertyChanged("AllAnnounc");
            }
        }

        private int _AnnounID;
        public int AnnounID
        {
            get { return _AnnounID; }
            set { _AnnounID = value; }
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

        private string _Announcement;
        public string Announcement
        {
            get { return _Announcement; }
            set
            {

                _Announcement = value;
                OnPropertyChanged("Announcement");

            }
        }


        private DateTime _TimeAnnounced;
        public DateTime TimeAnnounced
        {
            get { return _TimeAnnounced; }
            set
            {

                _TimeAnnounced = value;
                OnPropertyChanged("TimeAnnounced");

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

      
        public AnnoucnmentViewModel ()
        {

        }

        #region CRUD

        /// <summary>
        /// Fetches Announcement details
        /// </summary>
        public void GetAnnouncDetails ()

        {
            var announcDetails = WebAPI.GetCall(API_URIs.announcements);
            if (announcDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AllAnnounc = announcDetails.Result.Content.ReadAsAsync<ObservableCollection<Announcement>>().Result;
            }
        }

        /// <summary>
        /// Adds new Announcement
        /// </summary>
        public void CreateNewAnnounc ( int CourseID, string Announ, DateTime AnnoTime )
        {
            Announcement anno = new Announcement()
            {
                CourseID = CourseID,
                Announcement1 = Announ,
                TimeAnnounced = AnnoTime
            };

            var announcDetails = WebAPI.PostCall(API_URIs.announcements, anno);
            if (announcDetails.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                ResponseMessage = anno.AnnounID + "'s details has successfully been added!";
                MessageBox.Show("created ");
            }
            else
            {
                ResponseMessage = "Failed to update" + anno.AnnounID + "'s details.";
            }
        }


        /// <summary>
        /// Updates Announcement's record
        /// </summary>
        /// <param name="announcements"></param>
        public void UpdateAnnouncrDetails (int CourseID, string Announ, DateTime AnnoTime )
        {
            Announcement updateAnnounc = new Announcement()
            {
                CourseID = CourseID,
                Announcement1 = Announ,
                TimeAnnounced = AnnoTime
            };


            var announcDetails = WebAPI.PutCall(API_URIs.announcements + "?id=" + updateAnnounc.AnnounID, updateAnnounc);
            if (announcDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = updateAnnounc.AnnounID + "'s details has successfully been updated!";
            }
            else
            {
                ResponseMessage = "Failed to update" + updateAnnounc.AnnounID + "'s details.";
            }
        }

        /// <summary>
        /// Deletes Announcement's record
        /// </summary>
        public void DeleteAnnounceDetails ( int AnnounID )
        {

            Announcement deleteanno = new Announcement()
            {
                AnnounID = AnnounID
            };

            var announcDetails = WebAPI.DeleteCall(API_URIs.announcements + "?id=" + deleteanno.AnnounID);
            if (announcDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = deleteanno.AnnounID + "'s details has successfully been deleted!";
            }
            else
            {
                ResponseMessage = "Failed to delete" + deleteanno.AnnounID + "'s details.";
            }
        }
        #endregion

    }
    }
