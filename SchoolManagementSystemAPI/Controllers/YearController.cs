using SchoolManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolManagementSystemAPI.Controllers
{
    public class YearController : ApiController
    {
        private readonly Year yearModel = new Year();

        List<Year> year = new List<Year>();
    
        //public List<Year> Get()
        //{
        //    using (var db = new SchoolMSEntities())
        //    {
        //        var query = from year in db.Years
        //                    orderby year.YearID
        //                    select year;


        //        foreach (var item in query)
        //        {
        //            //yield return ("YearID: " + item.YearID + ", Year Number: " + item.YearNum);

        //            year.Add(new Year { YearID = item.YearID, YearNum = item.YearNum });
        //        }
        //        return year;
        //    }
        //}
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    using (var db = new SchoolMSEntities())
        //    {
        //        var query = from year in db.Years
        //                    orderby year.YearID
        //                    select year;

               
        //        foreach (var item in query)
        //        {
        //            //yield return ("YearID: " + item.YearID + ", Year Number: " + item.YearNum);

        //            year.Add(new Year { YearID = item.YearID, YearNum = item.YearNum });
        //        }
        //        return year;
        //    }
        //}

       
        [HttpGet]
        public IHttpActionResult Get ( int id )
        {
            var result = yearModel.GetYear(id);

            if (result == null)
            {
                return Content(HttpStatusCode.NotFound, "Year with Id: " + id + " not found");
            }

            return Ok("pk found");
        }

        [HttpGet]
        public IHttpActionResult Getall ( )
        {
            var result = yearModel.GetAll();

            if (result == null)
            {
                return Content(HttpStatusCode.NotFound, " not found");
            }

            return Ok(result);
        }

        // POST api/<controller>
        public HttpResponseMessage Post ( [FromBody] Year year )
        {
            try
            {
                yearModel.AddYear(year);

                var res = Request.CreateResponse(HttpStatusCode.Created, year);
                res.Headers.Location = new Uri(Request.RequestUri + year.YearID.ToString());
                return res;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        // PUT api/<controller>/5
        [HttpPut]
        public HttpResponseMessage Put ( int id, [FromBody] Year year )
        {
            try
            {

                if (yearModel.GetYear(id) != null)
                {

                    yearModel.UpdateYear(id, year);
                    var res = Request.CreateResponse(HttpStatusCode.OK, "Year with id" + id + " updated");
                    res.Headers.Location = new Uri(Request.RequestUri + year.YearID.ToString());
                    return res;

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Year with id " + id + " is not found!");
                }
               
              
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        // DELETE api/<controller>/5
        [HttpDelete]
        public HttpResponseMessage Delete ( int id )
        {
            try
            {
                if (yearModel.GetYear(id) != null)
                {
                    yearModel.DeleteYear(id);
                    return Request.CreateResponse(HttpStatusCode.OK, "Year with id " + id + " Deleted");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Year with id " + id + " is not found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}