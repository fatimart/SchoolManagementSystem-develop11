using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagementSystem.Models
{
    public class TimeTable
    {
        public int TimeTableID { get; set; }
        public int UserID { get; set; }
        public int CourseID { get; set; }
        public int SectionID { get; set; }
        public string RoomNo { get; set; }
        public int Year { get; set; }
        public string TeacherName { get; set; }
        public string CourseName { get; set; }
        public string Time { get; set; }
        public string CourseCode { get; set; }
        public int SectionNo { get; set; }
        public DateTime Examdate { get; set; }

        public virtual Course Course { get; set; }
        public virtual Section Section { get; set; }
        public virtual User User { get; set; }
    }
}
