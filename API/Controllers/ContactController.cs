using BusinessLogic;
using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ContactController : ApiController
    {
        private IManageContact _manageContact;
        public ContactController(IManageContact manageContact)
        {
            _manageContact = manageContact;
        }
        public ContactController()
        {
            _manageContact = new ManageContact();
        }


        // GET: api/Contact
        public HttpResponseMessage Get()
        {
            var data= _manageContact.GetAllContacts();
            var response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        // GET: api/Contact/5
        public HttpResponseMessage Get(int id)
        {
            var data = _manageContact.GetContact(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        // POST: api/Contact
        public HttpResponseMessage Post([FromBody]ContactDetail value)
        {
            var result=  _manageContact.AddContact(value);
            HttpResponseMessage response;
            if (result == 0)
            {
                response= Request.CreateResponse(HttpStatusCode.Conflict, "Contact Already Exist");
            }
            else
            {
                value.ID = result;
                response = Request.CreateResponse(HttpStatusCode.Created, value);
            }

            return response;
        }

        // PUT: api/Contact/5
        public HttpResponseMessage Put(int id, [FromBody]ContactDetail value)
        {
            HttpResponseMessage response;
            if (_manageContact.EditContact(id, value))
            {
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotModified, "Contact Already Exist with Same Details");
            }

            return response;
        }

        // DELETE: api/Contact/5
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage response;
            if (_manageContact.DeleteContact(id))
            {
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, "Contact not found");
            }

            return response;
        }
    }
}
