using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.ViewModels
{
    class SectionViewModel : ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
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

        public List<Section> GetAll1 ()
        {
            return ty.Sections.ToList();
        }

        public void GetAll ()
        {
            AllSections = new ObservableCollection<Section>();
            GetAll1().ForEach(data => AllSections.Add(new Section()
            {
                SectionID = data.SectionID,
                SectionNum = data.SectionNum,
                CourseID = data.CourseID,
                RoomID = data.RoomID,
                Time = data.Time
                

            }));

        }

        //MARK: DataAcesss
        public void AddSection(int sectionID, byte sectionNum, int courseID, int RoomID, string time)
        {

            Section section = new Section();
            section.SectionID = sectionID;
            section.SectionNum = sectionNum;
            section.CourseID = courseID;
            section.RoomID = RoomID;
            section.Time = time;

            ty.Sections.Add(section);
            ty.SaveChanges();

        }


        public void UpdateSection(int sectionID, byte sectionNum, int courseID, int RoomID, string time )
        {

            Section updateSection = (from m in ty.Sections where m.SectionID == sectionID select m).Single();
            updateSection.SectionID = sectionID;
            updateSection.SectionNum = sectionNum;
            updateSection.CourseID = courseID;
            updateSection.RoomID = RoomID;
            updateSection.Time = time;

            ty.SaveChanges();

        }

        public void DeleteSection(int sectionID)
        {

            var deleteSection = ty.Sections.Where(m => m.SectionID == sectionID).Single();
            ty.Sections.Remove(deleteSection);
            ty.SaveChanges();

        }

    }
}
