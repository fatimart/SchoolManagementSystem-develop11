using SchoolManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolManagementSystemAPI.Controllers
{
    public class RoomController : ApiController
    {     // GET api/<controller>
        public IEnumerable<string> Get()
        {
            using (var db = new SchoolMSEntities())
            {
                var query = from Room in db.Rooms
                            orderby Room.RoomID
                            select Room;


                foreach (var item in query)
                {
                    yield return ("RoomID: "+ item.RoomID+" Room Number: " +item.RoomNum);

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
                    var Room = entities.Rooms.FirstOrDefault(R => R.RoomID == id);
                    if (Room != null)
                    {
                        return Ok(Room);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "Room with Id: " + id + " not found");
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
                    var Room = entities.Rooms.Where(R => R.RoomID == id).FirstOrDefault();
                    if (Room != null)
                    {
                        entities.Rooms.Remove(Room);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Room with id " + id + " Deleted");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Room with id " + id + " is not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Post([FromBody] Room room)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    entities.Rooms.Add(room);
                    entities.SaveChanges();
                    var res = Request.CreateResponse(HttpStatusCode.Created, room);
                    res.Headers.Location = new Uri(Request.RequestUri + room.RoomID.ToString());
                    return res;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Room room)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var Room = entities.Rooms.Where(R => R.RoomID == id).FirstOrDefault();
                    if (Room != null)
                    {
                        if (!string.IsNullOrWhiteSpace(Room.RoomNum))
                            Room.RoomNum = room.RoomNum;


                        //if (year.YearID != 0 || year.YearID <= 0)
                        //   Year.YearID = year.YearID;

                        entities.SaveChanges();
                        var res = Request.CreateResponse(HttpStatusCode.OK, "Room with id" + id + " updated");
                        res.Headers.Location = new Uri(Request.RequestUri + Room.RoomID.ToString());
                        return res;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Room with id" + id + " is not found!");
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