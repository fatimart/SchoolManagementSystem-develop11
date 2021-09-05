using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
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
         public int YearID
        {
            get { return year.YearID; }
            set
            {
                if (year.YearID != value)
                {
                    year.YearID = value;
                    OnPropertyChanged("YearID");
                }
            }
        }

        public void AddYear(string YearNum)
        {

            Year year = new Year();
            year.YearNum = YearNum;


            ty.Years.Add(year);
            ty.SaveChanges();

        }


        public void UpdateYear(int YearID, string YearNum)
        {

            Year updateYears = (from m in ty.Years where m.YearID == YearID select m).Single();
            updateYears.YearID = YearID;
            updateYears.YearNum = YearNum;

            ty.SaveChanges();

        }

        public void DeleteYear(int YearID)
        {

            var deleteYears = ty.Years.Where(m => m.YearID == YearID).Single();
            ty.Years.Remove(deleteYears);
            ty.SaveChanges();

        }
        public bool CheckYearID(int roomID)
        {
            try
            {
                var RoomID = ty.Years.Where(m => m.YearID == YearID).Single();
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
