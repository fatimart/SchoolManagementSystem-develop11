using SchoolManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolManagementSystemAPI.Controllers
{
    public class StudentGradeController : ApiController
    {
        public IEnumerable<string> Get()
        {
            using (var db = new SchoolMSEntities())
            {
                var query = from StudentGrade in db.StudentGrades
                            orderby StudentGrade.ID
                            select StudentGrade;


                foreach (var item in query)
                {
                    yield return ("ID: "+item.ID+" CourseID: "+item.CourseID+" StudentID:"+item.StudentID+" Attendance: "+item.Attendance+" Score:"+item.Score+" Done:"+item.Done+" Year:"+item.year+" Section ID: "+item.SectionID  );
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
                    var Sgrades = entities.StudentGrades.FirstOrDefault(Sg => Sg.ID == id);
                    if (Sgrades != null)
                    {
                        return Ok(Sgrades);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "StudentGrade with ID: " + id + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }
        }

        /// <summary>  
        /// Creates a new employee  
        /// </summary>  

        /// <returns>details of newly created employee</returns>  
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] StudentGrade Sgrades)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    entities.StudentGrades.Add(Sgrades);
                    entities.SaveChanges();
                    var res = Request.CreateResponse(HttpStatusCode.Created, Sgrades);
                    res.Headers.Location = new Uri(Request.RequestUri + Sgrades.ID.ToString());
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
                    var Sgrades = entities.StudentGrades.Where(SG => SG.ID == id).FirstOrDefault();
                    if (Sgrades != null)
                    {
                        entities.StudentGrades.Remove(Sgrades);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "student Grade with id " + id + " Deleted");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "student Grade with id " + id + " is not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] StudentGrade Sgrades)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var SGrades = entities.StudentGrades.Where(sg => sg.ID == id).FirstOrDefault();
                    if (SGrades != null)
                    {
                        if (!string.IsNullOrWhiteSpace(Sgrades.ID.ToString()))

                            SGrades.CourseID = Sgrades.CourseID;
                        SGrades.StudentID = Sgrades.StudentID;
                        SGrades.Score = Sgrades.Score;
                        SGrades.Attendance = Sgrades.Attendance;
                        SGrades.Done = Sgrades.Done;
                        SGrades.year = Sgrades.year;
                        SGrades.SectionID = Sgrades.SectionID;



                        entities.SaveChanges();
                        var res = Request.CreateResponse(HttpStatusCode.OK, "student Grade with id" + id + " updated");
                        res.Headers.Location = new Uri(Request.RequestUri +Sgrades.ID.ToString());
                        return res;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "student Grade with id" + id + " is not found!");
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