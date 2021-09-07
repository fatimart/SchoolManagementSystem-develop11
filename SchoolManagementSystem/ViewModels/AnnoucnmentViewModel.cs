using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.ViewModels
{
    class AnnoucnmentViewModel : ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();

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


        public List<Announcement> GetAll1 ()
        {
            return ty.Announcements.ToList();

            //return ty.Announcements.Where(m => m.CourseID ==).ToList();
        }

        public AnnoucnmentViewModel ()
        {
            GetAll();
        }

        public void GetAll ()
        {
            AllAnnounc = new ObservableCollection<Announcement>();
            GetAll1().ForEach(data => AllAnnounc.Add(new Announcement()
            {
                AnnounID = Convert.ToInt32(data.AnnounID),
                CourseID = Convert.ToInt32(data.CourseID),
                Announcement1 = data.Announcement1,
                TimeAnnounced = Convert.ToDateTime(data.TimeAnnounced)

            }));

        }

    }
    }
