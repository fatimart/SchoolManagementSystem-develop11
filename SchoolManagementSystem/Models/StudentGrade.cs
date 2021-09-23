using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public class StudentGrade
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public int SectionID { get; set; }
        public int StudentID { get; set; }
        public int Score { get; set; }
        public int Attendance { get; set; }
        public bool Done { get; set; }
        public int year { get; set; }

        public virtual Course Course { get; set; }
        public virtual Section Section { get; set; }
        public virtual User User { get; set; }
    }
}
