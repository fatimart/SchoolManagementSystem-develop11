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
        private readonly StudentGrade sGradeModel = new StudentGrade();

        public IEnumerable<string> Get()
        {
            return sGradeModel.GetS();
        }


        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {

                var result = sGradeModel.GetById(id);

                if (result == null)
                {
                    return Content(HttpStatusCode.NotFound, "course with Id: " + id + " not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);

            }
        }

      
        [HttpPost]
        public HttpResponseMessage Post([FromBody] StudentGrade Sgrades)
        {
            try
            {
                sGradeModel.AddStudentGrade(Sgrades);

                var res = Request.CreateResponse(HttpStatusCode.Created, Sgrades);
                res.Headers.Location = new Uri(Request.RequestUri + Sgrades.ID.ToString());
                return res;

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
                if (sGradeModel.GetById(id) != null)
                {
                    sGradeModel.UpdateStudentGrade(id, Sgrades);

                    var res = Request.CreateResponse(HttpStatusCode.OK, "student Grade with id" + id + " updated");
                    res.Headers.Location = new Uri(Request.RequestUri + Sgrades.ID.ToString());
                    return res;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "student Grade with id" + id + " is not found!");
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
                if (sGradeModel.GetById(id) != null)
                {
                    //sGradeModel.deleteStudentGrade(id);

                    return Request.CreateResponse(HttpStatusCode.OK, "student Grade with id " + id + " Deleted");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "student Grade with id " + id + " is not found!");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}