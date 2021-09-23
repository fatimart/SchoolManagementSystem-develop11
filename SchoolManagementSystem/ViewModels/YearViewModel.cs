using SchoolManagementSystem.Models;
using SchoolManagementSystem.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class YearViewModel : ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
        public Year year;

        private List<Year> _year;

        public List<Year> Years
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged("AllYears");
            }
        }

        private ObservableCollection<Year> _yearRecord;
        public ObservableCollection<Year> AllYears
        {
            get
            {
                return _yearRecord;
            }
            set
            {
                _yearRecord = value;
                OnPropertyChanged("AllYears");
            }
        }


        private string _YearNum;
        public string YearNum
        {
            get { return _YearNum; }
            set
            {
                _YearNum = value;
                OnPropertyChanged("YearNum");

            }
        }

        private int _YearID;
        public int YearID
        {
            get { return _YearID; }
            set
            {
                _YearID = value;
                OnPropertyChanged("YearID");

            }
        }

        private string _responseMessage = "Welcome to REST API Tutorials";

        public string ResponseMessage
        {
            get { return _responseMessage; }
            set
            {
                _responseMessage = value;
                OnPropertyChanged("ResponseMessage");
            }
        }

        private string _showPostMessage = "Fill the form to add new year!";

        public string ShowPostMessage
        {
            get { return _showPostMessage; }
            set
            {
                _showPostMessage = value;
                OnPropertyChanged("ShowPostMessage");
            }
        }
        public YearViewModel()
        {
            //GetYearDetails();
            //GetAll1();
        }
        
        //MARK: Get All years
        public List<Year> GetAll1 ()
        {
            return ty.Years.ToList();


        }

        public void GetAll ()
        {
            AllYears = new ObservableCollection<Year>();
            GetAll1().ForEach(data => AllYears.Add(new Year()
            {
                YearID = data.YearID,
                YearNum = data.YearNum

            }));

        }

        #region CRUD

        /// <summary>
        /// Fetches Year details
        /// </summary>
        public void GetYearDetails ()

        {
            var yearDetails = WebAPI.GetCall(API_URIs.years);  
            if (yearDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)  
            {
                Years = yearDetails.Result.Content.ReadAsAsync<List<Year>>().Result;  
                //IsLoadData = true;  
            }
        }

        /// <summary>
        /// Adds new Year
        /// </summary>
        public void CreateNewYear ( string YearNum )
        {
            Year newYear = new Year()
            {
                YearNum = YearNum
            };

            var yearsDetails = WebAPI.PostCall(API_URIs.years, newYear);
            if (yearsDetails.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                ShowPostMessage = newYear.YearNum + "'s details has successfully been added!";
                MessageBox.Show("created ");
            }
            else
            {
                ShowPostMessage = "Failed to update" + newYear.YearNum + "'s details.";
            }
        }


        /// <summary>
        /// Updates Year's record
        /// </summary>
        /// <param name="years"></param>
        public void UpdateYearDetails ( int YearID, string YearNum )
        {
            Year updateYear = new Year()
            {
                YearID = YearID,
                YearNum = YearNum,

            };


            var yearsDetails = WebAPI.PutCall(API_URIs.years + "?id=" + updateYear.YearID, updateYear);
            if (yearsDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = updateYear.YearID + "'s details has successfully been updated!";
            }
            else
            {
                ResponseMessage = "Failed to update" + updateYear.YearID + "'s details.";
            }
        }

        /// <summary>
        /// Deletes Year's record
        /// </summary>
        public void DeleteYeareDetails ( int YearID )
        {

            Year deleteYear = new Year()
            {
                YearID = YearID
            };

            var yearsDetails = WebAPI.DeleteCall(API_URIs.years + "?id=" + deleteYear.YearID);
            if (yearsDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = deleteYear.YearID + "'s details has successfully been deleted!";
            }
            else
            {
                ResponseMessage = "Failed to delete" + deleteYear.YearID + "'s details.";
            }
        }
        #endregion

    }
}
