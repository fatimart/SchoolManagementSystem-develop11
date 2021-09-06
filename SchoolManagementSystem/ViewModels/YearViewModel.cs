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
    class YearViewModel : ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
        public Year year;


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

        public YearViewModel()
        {
            GetAll();
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

        //MARK: DataAccess Functions
        public void AddYear ( string YearNum )
        {

            Year year = new Year();
            year.YearNum = YearNum;


            ty.Years.Add(year);
            ty.SaveChanges();
            GetAll();

        }

        public void UpdateYear(int YearID, string YearNum)
        {

            Year updateYears = (from m in ty.Years where m.YearID == YearID select m).Single();
            updateYears.YearID = YearID;
            updateYears.YearNum = YearNum;

            ty.SaveChanges();
            GetAll();

        }

        public void DeleteYear(int YearID)
        {

            var deleteYears = ty.Years.Where(m => m.YearID == YearID).Single();
            ty.Years.Remove(deleteYears);
            ty.SaveChanges();
            GetAll();

        }

        public bool CheckYearID(int YearID)
        { 

            if (ty.Years.Any(o => o.YearID == YearID))
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }
}
