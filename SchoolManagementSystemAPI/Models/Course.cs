namespace SchoolManagementSystemAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public partial class Course
    {
        public SchoolMSEntities ty = new SchoolMSEntities();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            this.Announcements = new HashSet<Announcement>();
            this.Documents = new HashSet<Document>();
            this.Sections = new HashSet<Section>();
            this.StudentGrades = new HashSet<StudentGrade>();
            this.TeacherCourses = new HashSet<TeacherCours>();
            this.TimeTables = new HashSet<TimeTable>();
        }

        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string Description { get; set; }
        public System.DateTime ExamDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Announcement> Announcements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Section> Sections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeacherCours> TeacherCourses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTable> TimeTables { get; set; }

        private ObservableCollection<Course> _courseRecords;
        public ObservableCollection<Course> AllCourses
        {
            get
            {
                return _courseRecords;
            }
            set
            {
                _courseRecords = value;
                //OnPropertyChanged("AllCourses");
            }
        }

        public Course GetCourse(int CourseID)
        {
            return ty.Courses
                     .Where(s => s.CourseID == CourseID)
                     .FirstOrDefault() as Course;

        }

        public Course Get(int id)
        {
            return ty.Courses.Find(id);
        }

        public Course GetCourseCode(string CourseCode)
        {
            return ty.Courses
                .Where(s => s.CourseCode == CourseCode)
                .FirstOrDefault();
        }


        public List<Course> GetAll1()
        {
            return ty.Courses.ToList();
        }

        public void GetAll()
        {
            AllCourses = new ObservableCollection<Course>();
            GetAll1().ForEach(data => AllCourses.Add(new Course()
            {
                CourseID = Convert.ToInt32(data.CourseID),
                CourseName = data.CourseName,
                CourseCode = data.CourseCode,
                Description = data.Description,
                ExamDate = Convert.ToDateTime(data.ExamDate),

            }));

        }

        public void AddCourse(Course course1)
        {
            ty.Courses.Add(course1);
            ty.SaveChanges();

        }

        public void UpdateCourse(int CourseId, Course updateCourse)
        {

            var result = (from m in ty.Courses where m.CourseID == CourseId select m).Single();
            if (result != null)
            {
                result.CourseName = updateCourse.CourseName;
                result.CourseCode = updateCourse.CourseCode;
                result.Description = updateCourse.Description;
                result.ExamDate = updateCourse.ExamDate;

                ty.SaveChanges();

            }
        }


        //MARK: Update by course code 
        public void UpdateCourseV2(Course updateCourse)
        {

            var result = (from m in ty.Courses where m.CourseCode == CourseCode select m).Single();
            if (result != null)
            {
                result.CourseName = updateCourse.CourseName;
                result.CourseCode = updateCourse.CourseCode;
                result.Description = updateCourse.Description;
                result.ExamDate = updateCourse.ExamDate;

                ty.SaveChanges();

            }

        }


        public void DeleteCourse(int courseID)
        {
            var deleteCourse = ty.Courses.Where(m => m.CourseID == courseID).Single();
            ty.Courses.Remove(deleteCourse);
            ty.SaveChanges();

        }



        public bool checkCourseCode(string CourseCode)
        {
            if (ty.Courses.Any(o => o.CourseCode == CourseCode))
            { return true; }
            else
            {
                return false;
            }
        }

        //MARK: update and insert when uploading new excel sheet
        //public void UpdateCourse1 ( string courseName, string courseCode, string description, DateTime examDate )
        //{

        //    if (ty.Courses.Any(o => o.CourseCode == courseCode))
        //    {
        //        UpdateCourseV2(courseName, courseCode, description, examDate);
        //    }

        //    else
        //    {
        //        AddCourse(courseName, courseCode, description, examDate);
        //    }
        //}

        public void ResetData()
        {
            Course course1 = new Course();

            course1.CourseID = 0;
            course1.CourseCode = string.Empty;
            course1.CourseName = string.Empty;
            course1.Description = string.Empty;

        }


    }
}