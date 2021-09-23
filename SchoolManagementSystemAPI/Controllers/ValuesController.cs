using SchoolManagementSystemAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SchoolManagementSystemAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        public IEnumerable<User> allUsers()
        {
            using (SchoolMSEntities ty = new SchoolMSEntities())
            {
                return ty.Users.ToList();
            }
        }
        public IEnumerable<User> Get(string UserName)
        {
            using (SchoolMSEntities ty = new SchoolMSEntities())
            {
                yield return ty.Users.FirstOrDefault(p => p.UserName == UserName);
            }
        }

    }
}
