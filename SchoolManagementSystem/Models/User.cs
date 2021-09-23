using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal CPR { get; set; }
        public string Address { get; set; }
        public System.DateTime DOB { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }
    }
}
