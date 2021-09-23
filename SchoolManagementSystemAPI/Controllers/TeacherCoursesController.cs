using SchoolManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolManagementSystemAPI.Controllers
{
    public class TeacherCoursesController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            using (var db = new SchoolMSEntities())
            {
                var query = from TeacherCours in db.TeacherCourses
                            orderby TeacherCours.TID
                            select TeacherCours;


                foreach (var item in query)
                {
                    yield return  ("teacherCourse ID: "+item.TID+" CourseID: "+item.CourseID+" SectionID: "+item.SectionID+" CourseCode:"+item.CourseCode+" UserID: "+item.UserID+" Teacher Name:"+item.TeacherName);

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
                    var teacherCourse = entities.TeacherCourses.FirstOrDefault(y => y.TID == id);
                    if (teacherCourse != null)
                    {
                        return Ok(teacherCourse);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "teacherCourse with Id: " + id + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (SchoolMSEntities ty = new SchoolMSEntities())
                {
                    var DeleteTeacherCourses = ty.TeacherCourses.Where(m => m.TID == id).Single();
                  
                    if (DeleteTeacherCourses != null)
                    {
                        ty.TeacherCourses.Remove(DeleteTeacherCourses);
                        ty.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "TeacherCourse with id " + id + " Deleted");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "TeacherCourse with id " + id + " is not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage Post([FromBody] TeacherCours teacherCourse)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    entities.TeacherCourses.Add(teacherCourse);
                    entities.SaveChanges();
                    var res = Request.CreateResponse(HttpStatusCode.Created, teacherCourse);
                    res.Headers.Location = new Uri(Request.RequestUri + teacherCourse.TID.ToString());
                    return res;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] TeacherCours teacherCourse)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                  
                   

                    var TeacherCourse = entities.TeacherCourses.Where(m => m.TID == id).FirstOrDefault();
                    if (TeacherCourse != null)
                    {
                    

                        entities.SaveChanges();
                        var res = Request.CreateResponse(HttpStatusCode.OK, "TeacherCourse with id" + id + " updated");
                        res.Headers.Location = new Uri(Request.RequestUri + teacherCourse.TID.ToString());
                        return res;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "TeacherCourse with id" + id + " is not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public IHttpActionResult FillCourseDetails()
        {
            try
            {

                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("select * from TeacherCourses ", connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return Ok(dt);
                  //  connection.Close();
                }
            }

            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, "Something Wrong");

            }
        }

    }
}