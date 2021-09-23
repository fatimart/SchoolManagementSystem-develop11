using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public class TeacherCours
    {
        public int TID { get; set; }
        public int CourseID { get; set; }
        public int SectionID { get; set; }
        public int UserID { get; set; }
        public string CourseCode { get; set; }
        public string TeacherName { get; set; }

        public virtual Course Course { get; set; }
        public virtual Section Section { get; set; }
        public virtual User User { get; set; }
    }
}
