using SchoolManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolManagementSystemAPI.Controllers
{
    public class TeacherController : ApiController
    {
        private readonly Teacher teacherModel = new Teacher();

        public IEnumerable<string> Get ()
        {
            using (var db = new SchoolMSEntities())
            {
                var query = from user in db.Users
                            orderby user.UserID
                            select user;


                foreach (var item in query)
                {
                    yield return ("UserID: " + item.UserID + " UserName: " + item.UserName + ",     Name: " + item.Name);
                }
            }
        }


        [HttpGet]
        public IHttpActionResult Get ( int id )
        {
            try
            {
                var result = teacherModel.Get(id);

                if (result == null)
                {
                    return Content(HttpStatusCode.NotFound, "user with Id: " + id + " not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }
        }


        /// <returns>details of newly created employee</returns>  
        [HttpPost]
        public HttpResponseMessage Post ( [FromBody] User user )
        {
            try
            {
                teacherModel.InserTeacher(user);
                var res = Request.CreateResponse(HttpStatusCode.Created, user);
                res.Headers.Location = new Uri(Request.RequestUri + user.UserID.ToString());
                return res;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        public HttpResponseMessage Put ( int id, [FromBody] User user )
        {
            try
            {
                if (teacherModel.Get(id) != null)
                {
                    teacherModel.UpdateUser(id, user);

                    var res = Request.CreateResponse(HttpStatusCode.OK, "user with id" + id + " updated");
                    res.Headers.Location = new Uri(Request.RequestUri + user.UserID.ToString());
                    return res;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user with id" + id + " is not found!");
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpDelete]
        public HttpResponseMessage Delete ( int id )
        {
            try
            {
                if (teacherModel.Get(id) != null)
                {
                    teacherModel.DeleteUser(id);
                    return Request.CreateResponse(HttpStatusCode.OK, "user with id " + id + " Deleted");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user with id " + id + " is not found!");
                }
            
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}