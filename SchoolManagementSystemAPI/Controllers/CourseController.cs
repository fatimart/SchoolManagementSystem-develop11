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
        public SchoolMSEntities ty = new SchoolMSEntities();

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
        [Route("api/courses/CheckCourseCode/")]
        [HttpGet]
        public HttpResponseMessage Get ( string CourseCode )
        {
            try
            {
                var result = courseModel.GetCourseCode(CourseCode);

                if (result == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Course with id" + CourseCode + " is not found!");
                }
                else
                {
                    return  Request.CreateResponse(HttpStatusCode.OK, "Course with id" + CourseCode + " found");

                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

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