using SchoolManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace SchoolManagementSystemAPI.Controllers
{
    public class UsersController : ApiController
    {
        public IEnumerable<string> Get()
        {
            using (var db = new SchoolMSEntities())
            {
                var query = from user in db.Users
                            orderby user.UserID
                            select user;


                foreach (var item in query)
                {
                    yield return ("UserID: "+item.UserID+" UserName: " + item.UserName + ",     Name: " + item.Name);
                }
            }
        }


        [System.Web.Http.HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                { 
                      var user = entities.Users.FirstOrDefault(p => p.UserID == id);
                    if (user != null)
                    {
                       User user1 = new User
                        {
                            UserID = user.UserID,
                            UserName = user.UserName,
                           Name = user.Name,
                           Email =user.Email,
                           CPR = user.CPR,
                           Address =user.Address,
                           DOB = user.DOB,
                           Type = user.Type,
                           Password = "Private Inforamtion",
                           ContactNo = user.ContactNo,
                       };
                      
                        return Ok(user1);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "user with UserID: " + id + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }
        }

      
        /// <returns>details of newly created employee</returns>  
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] User user)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    entities.Users.Add(user);
                    entities.SaveChanges();
                    var res = Request.CreateResponse(HttpStatusCode.Created, user);
                    res.Headers.Location = new Uri(Request.RequestUri + user.UserID.ToString());
                    return res;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var user = entities.Users.Where(u => u.UserID == id).FirstOrDefault();
                    if (user != null)
                    {
                        entities.Users.Remove(user);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "user with id " + id + " Deleted");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user with id " + id + " is not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] User user)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var User = entities.Users.Where(u => u.UserID == id).FirstOrDefault();
                    if (User != null)
                    {
                        if (!string.IsNullOrWhiteSpace(user.UserID.ToString()))

                        User.UserName = user.UserName;
                        User.Name = user.Name;
                        User.Email = user.Email;
                        User.CPR = user.CPR;
                        User.Address = user.Address;
                        User.DOB = user.DOB;
                       // User.Type = "Student";
                        User.Password = user.Password;
                        User.ContactNo = user.ContactNo;



                        entities.SaveChanges();
                        var res = Request.CreateResponse(HttpStatusCode.OK, "user with id" + id + " updated");
                        res.Headers.Location = new Uri(Request.RequestUri + user.UserID.ToString());
                        return res;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user with id" + id + " is not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //public IEnumerable<User> Get()
        //{

        //        var query = from p in ty.Users select new User
        //        {
        //            UserID = p.UserID,
        //            UserName = p.UserName,
        //            Name = p.Name,
        //            Email = p.Email,
        //            CPR = p.CPR,
        //            Address =p.Address,
        //            DOB = p.DOB,
        //            Type = p.Type,
        //            Password = p.Password,
        //            ContactNo = p.ContactNo,
        //        };
        //    return query.ToArray();
        //}
        //public User Get(int id)
        //{
        //    using (SchoolMSEntities1 ty = new SchoolMSEntities1())
        //    {


        //        return ty.Users.FirstOrDefault(p => p.UserID == id);
        //    }
        //}

    }
}