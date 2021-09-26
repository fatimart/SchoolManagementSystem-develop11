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
        SchoolMSEntities entities1 = new SchoolMSEntities();
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
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var course = entities.Courses.FirstOrDefault(c => c.CourseID == id);
                    if (course != null)
                    {
                        return Ok(course);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "course with courseID: " + id + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }
        }

        [Route("{id:int}/details")]
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> GetCourseDetail ( int id )
        {
            var book = await (from b in entities1.Courses
                              where b.CourseID == id
                              select new Course
                              {
                                  CourseID = b.CourseID,
                                  CourseName = b.CourseName,
                                  CourseCode = b.CourseCode,
                                  Description = b.Description,
                                  ExamDate = b.ExamDate

                              }).FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }


        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Course course)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    entities.Courses.Add(course);
                    entities.SaveChanges();
                    var res = Request.CreateResponse(HttpStatusCode.Created, course);
                    //res.Headers.Location = new Uri(Request.RequestUri + course.CourseID.ToString());
                    return res;
                }
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
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var Course = entities.Courses.Where(c => c.CourseID == id).FirstOrDefault();
                    if (Course != null)
                    {
                        if (!string.IsNullOrWhiteSpace(course.CourseID.ToString()))
                     
                        Course.CourseName = course.CourseName;
                        Course.CourseCode = course.CourseCode;
                        Course.Description = course.Description;
                        Course.ExamDate = course.ExamDate;

                        entities.SaveChanges();
                        var res = Request.CreateResponse(HttpStatusCode.OK, "Course with id" + id + " updated");
                        res.Headers.Location = new Uri(Request.RequestUri + course.CourseID.ToString());
                        return res;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Course with id" + id + " is not found!");
                    }
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
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var Course = entities.Courses.Where(c => c.CourseID == id).FirstOrDefault();
                    if (Course != null)
                    {
                        entities.Courses.Remove(Course);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Course with id " + id + " Deleted");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Course with id " + id + " is not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



    }
}