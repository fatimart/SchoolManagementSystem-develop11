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
        public void AddSection( int sectionNum, int courseID, int RoomID, string time)
        {
            if (!checkSectionExists(sectionNum, courseID))
            {
                Section section = new Section();
                section.SectionNum = sectionNum;
                section.CourseID = courseID;
                section.RoomID = RoomID;
                section.Time = time;

                ty.Sections.Add(section);
                ty.SaveChanges();
            }
            else
            {
                MessageBox.Show("Section with course code already exists please choose another section number!");

            }
        }


        public void UpdateSection(int sectionNum, int courseID, int RoomID, string time )
        {
            
                Section updateSection = (from m in ty.Sections
                                         where m.SectionNum == sectionNum
                                         where m.CourseID == courseID
                                         select m).Single();

                updateSection.SectionNum = sectionNum;
                updateSection.CourseID = courseID;
                updateSection.RoomID = RoomID;
                updateSection.Time = time;

                ty.SaveChanges();
            
           
        }

        public void DeleteSection ( int sectionNum, int courseID )
        {

            if (checkSectionExists(sectionNum, courseID))
            { 
                var deleteSection = (from m in ty.Sections
                                     where m.SectionNum == sectionNum
                                     where m.CourseID == courseID
                                     select m).Single();
            
                ty.Sections.Remove(deleteSection);
                ty.SaveChanges();
        }
            else
            {
                MessageBox.Show("course with section number " + sectionNum + " does not exists!");

            }

        }

        public bool checkSectionExists ( int sectionNum, int CourseID )
        {
            if (ty.Sections.Any(o => o.CourseID == CourseID && o.SectionNum == sectionNum))
            { return true; }
            else
            {
                return false;
            }
        }

    }
}
