using SchoolManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolManagementSystemAPI.Controllers
{
    public class DocumentsController : ApiController
    {

        List<Document> Documents = new List<Document>();

        public List<Document> Get()
        {
            using (var db = new SchoolMSEntities())
            {
                var query = from Document in db.Documents
                            orderby Document.ID
                            select Document;


                foreach (var item in query)
                {
                    //yield return ("YearID: " + item.YearID + ", Year Number: " + item.YearNum);

                    Documents.Add(new Document { Extension = item.Extension, Data = item.Data, CourseID = item.CourseID, ID = item.ID, FileName = item.FileName });
                }
                return Documents;
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
                    var documents = entities.Documents.FirstOrDefault(y => y.ID == id);
                    if (documents != null)
                    {
                        return Ok(documents);
                    }
                    else
                    {
                        return Content(HttpStatusCode.NotFound, "documents with Id: " + id + " not found");
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
                    var documents = entities.Documents.Where(y => y.ID == id).FirstOrDefault();
                    if (documents != null)
                    {
                        entities.Documents.Remove(documents);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Documents with id " + id + " Deleted");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Documents with id " + id + " is not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Post([FromBody] Document documents)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    entities.Documents.Add(documents);
                    entities.SaveChanges();
                    var res = Request.CreateResponse(HttpStatusCode.Created, documents);
                    res.Headers.Location = new Uri(Request.RequestUri + documents.ID.ToString());
                    return res;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Document documents)
        {
            try
            {
                using (SchoolMSEntities entities = new SchoolMSEntities())
                {
                    var Document = entities.Documents.Where(y => y.ID == id).FirstOrDefault();
                    if (Document != null)
                    {
                        if (!string.IsNullOrWhiteSpace(documents.ID.ToString()))
                            Document.ID = documents.ID;
                        Document.CourseID = documents.CourseID;
                        Document.Data = documents.Data;
                        Document.FileName = documents.FileName;


                        //if (year.YearID != 0 || year.YearID <= 0)
                        //   Year.YearID = year.YearID;

                        entities.SaveChanges();
                        var res = Request.CreateResponse(HttpStatusCode.OK, "Document with id" + id + " updated");
                        res.Headers.Location = new Uri(Request.RequestUri + Document.ID.ToString());
                        return res;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Document with id" + id + " is not found!");
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