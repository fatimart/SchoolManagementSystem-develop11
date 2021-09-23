using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public class Document
    {
        public int ID { get; set; }
        public byte[] Data { get; set; }
        public string Extension { get; set; }
        public int CourseID { get; set; }
        public string FileName { get; set; }

        public virtual Course Course { get; set; }
    }
}
