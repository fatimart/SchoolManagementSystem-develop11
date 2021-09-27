using SchoolManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SchoolManagementSystemAPI.Controllers
{
    public class CourseController : ApiController
    {
        private readonly Course courseModel = new Course();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            using (var db = new SchoolMSEntities())
            {
                var query = from Course in db.Courses
                            orderby Course.CourseID
                            select Course;


                foreach (var item in query)
                {
                    yield return ("course: " + item.CourseID + ",course Code: " + item.CourseCode);

                }
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = courseModel.Get(id);

                if (result == null)
                {
                    return Content(HttpStatusCode.NotFound, "course with Id: " + id + " not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }
        }



        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Course course)
        {
            try
            {

                courseModel.AddCourse(course);
                var res = Request.CreateResponse(HttpStatusCode.Created, course);
                res.Headers.Location = new Uri(Request.RequestUri + course.CourseID.ToString());
                return res;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Course course)
        {
            try
            {
                if (courseModel.Get(id) != null)
                {
                    courseModel.UpdateCourse(id, course);

                    var res = Request.CreateResponse(HttpStatusCode.OK, "Course with id" + id + " updated");
                    res.Headers.Location = new Uri(Request.RequestUri + course.CourseID.ToString());
                    return res;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Course with id" + id + " is not found!");
                }
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
      
     

        // DELETE api/<controller>/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (courseModel.Get(id) != null)
                {
                    courseModel.DeleteCourse(id);
                    return Request.CreateResponse(HttpStatusCode.OK, "Course with id " + id + " Deleted");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Course with id " + id + " is not found!");
                }
                }
            
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



    }
}