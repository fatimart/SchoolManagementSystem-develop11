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
    public class SectionsController : ApiController
    {
        
            List<Section> sections = new List<Section>();

            public List<Section> Get()
            {
                using (var db = new SchoolMSEntities())
                {
                    var query = from Section in db.Sections
                                orderby Section.SectionID
                                select Section;


                    foreach (var item in query)
                    {
                        //yield return ("YearID: " + item.YearID + ", Year Number: " + item.YearNum);

                        sections.Add(new Section { SectionID = item.SectionID, SectionNum = item.SectionNum,CourseID=item.CourseID,RoomID=item.RoomID,Time=item.Time });
                    }
                    return sections;
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
                        var section = entities.Sections.FirstOrDefault(y => y.SectionID == id);
                        if (section != null)
                        {
                            return Ok(section);
                        }
                        else
                        {
                            return Content(HttpStatusCode.NotFound, "section with Id: " + id + " not found");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.BadRequest, ex);

                }
            }


            public HttpResponseMessage Post([FromBody] Section section)
            {
                try
                {
                    using (SchoolMSEntities entities = new SchoolMSEntities())
                    {
                        entities.Sections.Add(section);
                        entities.SaveChanges();
                        var res = Request.CreateResponse(HttpStatusCode.Created, section);
                        res.Headers.Location = new Uri(Request.RequestUri + section.SectionID.ToString());
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
            [HttpPut]
            public HttpResponseMessage Put(int id, [FromBody] Section section)
            {
                try
                {
                    using (SchoolMSEntities entities = new SchoolMSEntities())
                    {
                        var Section = entities.Sections.Where(y => y.SectionID == id).FirstOrDefault();
                    if (Section != null)
                    {
                        //  if (!string.IsNullOrWhiteSpace(section.SectionID.ToString())) {
                        Section.SectionNum = section.SectionNum;
                        Section.CourseID = section.CourseID;
                        Section.RoomID = section.RoomID;
                        Section.Time = section.Time;
                    

                            //if (year.YearID != 0 || year.YearID <= 0)
                            //   Year.YearID = year.YearID;

                                    entities.SaveChanges();
                            var res = Request.CreateResponse(HttpStatusCode.OK, "section with id" + id + " updated");
                            res.Headers.Location = new Uri(Request.RequestUri + section.SectionID.ToString());
                            return res;
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "section with id" + id + " is not found!");
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
                        var sections = entities.Sections.Where(y => y.SectionID == id).FirstOrDefault();
                        if (sections != null)
                        {
                            entities.Sections.Remove(sections);
                            entities.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, "sections with id " + id + " Deleted");
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "sections with id " + id + " is not found!");
                        }
                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }

            }
     
        [Route ("api/sections/FillCourseBox")]
        [HttpGet]
        public IHttpActionResult FillCourseBox()
        {
            try
            {
              

                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT CourseCode, CourseID from Course;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the courses names
                con.Close();
                return Ok(dt);
                

            
            }

            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("api/sections/FillRoomBox")]
        [HttpGet]
        public IHttpActionResult FillRoomBox()
        {
            try
            {
       

                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT RoomNum,RoomID from Room;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the courses names
                con.Close();
                return Ok(dt);

               
            }

            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }
        }
        [Route("api/sections/FillDataGrid")]
        [HttpGet]
        public IHttpActionResult FillDataGrid()

        {
            try
            {

                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand command = new SqlCommand(
                       "select Section.SectionID, Section.Time, Course.CourseCode, Section.SectionNum, Room.RoomNum from Section,Course,Room where Section.CourseID=Course.CourseID AND Section.RoomID=Room.RoomID", con);
                    con.Open();

                    SqlDataAdapter sda = new SqlDataAdapter(command);

                    DataTable dt = new DataTable();
                    con.Close();
                    sda.Fill(dt);
                    return Ok(dt);

                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }

        }

        [Route("api/sections/getcourseIDD")]
        [HttpGet]
        public IHttpActionResult getcourseIDD(string CCodeBox)
        {

            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT CourseID from Course where CourseCode='" + CCodeBox + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the courses names

              int courseID = Convert.ToInt32(dt.Rows[0]["CourseID"].ToString());
                con.Close();
                return Ok(courseID);

              
            }

            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }
        }


        [Route("api/sections/getRoomIDD")]
        [HttpGet]
        public IHttpActionResult getRoomIDD(string RoomNumBox)
        {

            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT RoomID from Room where RoomNum='" + RoomNumBox + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); //db have all the courses names

                int RoomID = Convert.ToInt32(dt.Rows[0]["RoomID"].ToString());
                con.Close();
                return Ok(RoomID);


            }

            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }
        }




    }
}