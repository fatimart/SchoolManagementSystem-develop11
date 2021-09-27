using SchoolManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolManagementSystemAPI.Controllers
{
    public class TimeTableController : ApiController
    {
        private readonly TimeTable timeTableModel = new TimeTable();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            using (var db = new SchoolMSEntities())
            {
                var query = from year in db.TimeTables
                            orderby year.TimeTableID
                            select year;


                foreach (var item in query)
                {
                  yield return ("TimeTableID: " + item.TimeTableID);

                }
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = timeTableModel.GetTimeTable(id);

                if (result == null)
                {
                    return Content(HttpStatusCode.NotFound, "TimeTable reference with Id: " + id + " not found");
                }

                return Ok(result);


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
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var Timetable = entities.TimeTables.Where(y => y.TimeTableID == id).FirstOrDefault();
                    if (Timetable != null)
                    {
                        entities.TimeTables.Remove(Timetable);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Timetable reference with id " + id + " Deleted");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Timetable reference with id " + id + " is not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Post([FromBody] TimeTable Timetable)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    entities.TimeTables.Add(Timetable);
                    entities.SaveChanges();
                    var res = Request.CreateResponse(HttpStatusCode.Created, Timetable);
                    res.Headers.Location = new Uri(Request.RequestUri + Timetable.TimeTableID.ToString());
                    return res;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] TimeTable Timetable)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var updaTimeTables = entities.TimeTables.Where(y => y.TimeTableID == id).FirstOrDefault();
                    if (updaTimeTables != null)
                    {
                        if (!string.IsNullOrWhiteSpace(Timetable.TimeTableID.ToString()))
                        updaTimeTables.CourseID = Timetable.CourseID;
                        updaTimeTables.RoomNo = Timetable.RoomNo;
                        updaTimeTables.Year = Timetable.Year;
                        updaTimeTables.TimeTableID = Timetable.TimeTableID;
                        updaTimeTables.TeacherName = Timetable.TeacherName;

                        //if (year.YearID != 0 || year.YearID <= 0)
                        //   Year.YearID = year.YearID;

                        entities.SaveChanges();
                        var res = Request.CreateResponse(HttpStatusCode.OK, "TimeTable with id" + id + " updated");
                        res.Headers.Location = new Uri(Request.RequestUri + Timetable.TimeTableID.ToString());
                        return res;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "TimeTable with id" + id + " is not found!");
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