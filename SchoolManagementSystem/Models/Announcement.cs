using System;
using System.Collections.ObjectModel;

namespace SchoolManagementSystem.Models
{
    public class Announcement
    {
        public int AnnounID { get; set; }
        public int CourseID { get; set; }
        public string Announcement1 { get; set; }
        public DateTime TimeAnnounced { get; set; }
        public virtual Course Course { get; set; }

    }
}
