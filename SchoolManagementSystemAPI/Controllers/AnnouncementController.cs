using SchoolManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolManagementSystemAPI.Controllers
{
    public class AnnouncementController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            using (var db = new SchoolMSEntities())
            {
                var query = from Announcement in db.Announcements
                            orderby Announcement.AnnounID
                            select Announcement;


                foreach (var item in query)
                {
                    yield return (" AnnounceID: " + item.AnnounID+ " CourseID: " + item.CourseID+ " Announcement: " + item.Announcement1+ " Time Announced: " + item.TimeAnnounced);

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
                    var announcement = entities.Announcements.FirstOrDefault(y => y.AnnounID == id);
                    if (announcement != null)
                    {
                        return Ok(announcement);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "Announcement with Id: " + id + " not found");
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
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var announcement = entities.Announcements.Where(y => y.AnnounID == id).FirstOrDefault();
                    if (announcement != null)
                    {
                        entities.Announcements.Remove(announcement);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Announcement with id " + id + " Deleted");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Announcement with id " + id + " is not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Post([FromBody] Announcement announcement)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    entities.Announcements.Add(announcement);
                    entities.SaveChanges();
                    var res = Request.CreateResponse(HttpStatusCode.Created, announcement);
                    res.Headers.Location = new Uri(Request.RequestUri + announcement.AnnounID.ToString());
                    return res;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Announcement announcement)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var Announcement = entities.Announcements.Where(y => y.AnnounID == id).FirstOrDefault();
                    if (Announcement != null)
                    {
                        if (!string.IsNullOrWhiteSpace(announcement.AnnounID.ToString()))
                            
                        Announcement.AnnounID = announcement.AnnounID;
                        Announcement.CourseID = announcement.CourseID;
                        Announcement.Announcement1 = announcement.Announcement1;
                        Announcement.TimeAnnounced = announcement.TimeAnnounced;

                        //if (year.YearID != 0 || year.YearID <= 0)
                        //   Year.YearID = year.YearID;

                        entities.SaveChanges();
                        var res = Request.CreateResponse(HttpStatusCode.OK, "announcement with id" + id + " updated");
                        res.Headers.Location = new Uri(Request.RequestUri + announcement.AnnounID.ToString());
                        return res;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "announcement with id" + id + " is not found!");
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