using System;

namespace SchoolManagementSystem.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string Description { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
