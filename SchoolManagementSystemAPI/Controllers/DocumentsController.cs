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
        private readonly Document documentModel = new Document();

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
                var result = documentModel.GetById(id);

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

      
        public HttpResponseMessage Post([FromBody] Document documents)
        {
            try
            {
                documentModel.AddNewDocument(documents);

                var res = Request.CreateResponse(HttpStatusCode.Created, documents);
                res.Headers.Location = new Uri(Request.RequestUri + documents.ID.ToString());
                return res;

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
                if (documentModel.GetById(id) != null)
                {
                    documentModel.UpdateDocument(id, documents);


                    var res = Request.CreateResponse(HttpStatusCode.OK, "Document with id" + id + " updated");
                    res.Headers.Location = new Uri(Request.RequestUri + documents.ID.ToString());
                    return res;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Document with id" + id + " is not found!");
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
                if (documentModel.GetById(id) != null)
                {
                    documentModel.deleteDocument(id);

                    return Request.CreateResponse(HttpStatusCode.OK, "Documents with id " + id + " Deleted");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Documents with id " + id + " is not found!");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}