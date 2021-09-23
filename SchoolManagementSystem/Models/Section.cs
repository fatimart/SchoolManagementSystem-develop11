using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public class Section
    {
        public int SectionID { get; set; }
        public int SectionNum { get; set; }
        public int CourseID { get; set; }
        public int RoomID { get; set; }
        public string Time { get; set; }
    }
}
