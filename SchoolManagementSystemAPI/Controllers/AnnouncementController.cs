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
        private readonly Announcement announcementModel = new Announcement();

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
                var result = announcementModel.Get(id);

                if (result == null)
                {
                    return Content(HttpStatusCode.NotFound, "Announcement with Id: " + id + " not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }
        }

        
        public HttpResponseMessage Post([FromBody] Announcement announcement)
        {
            try
            {
                announcementModel.AddAnnoun(announcement);

                var res = Request.CreateResponse(HttpStatusCode.Created, announcement);
                res.Headers.Location = new Uri(Request.RequestUri + announcement.AnnounID.ToString());
                return res;

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
                if (announcementModel.Get(id) != null)
                {
                    announcementModel.UpdateAnnoun(id, announcement);


                    var res = Request.CreateResponse(HttpStatusCode.OK, "announcement with id" + id + " updated");
                    res.Headers.Location = new Uri(Request.RequestUri + announcement.AnnounID.ToString());
                    return res;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "announcement with id" + id + " is not found!");
                
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

                if (announcementModel.Get(id) != null)
                {
                    announcementModel.DeleteAnnoun(id);

                    return Request.CreateResponse(HttpStatusCode.OK, "Announcement with id " + id + " Deleted");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Announcement with id " + id + " is not found!");
                    }
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}