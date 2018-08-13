using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using System.Net.Http;
using System.Web.Http;
using Entities;
using API;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using Interfaces;
using Moq;
using System.Collections.Generic;

namespace UnitTest.API
{
    [TestClass]
    public class ContactTest
    {
        Mock<IManageContact> mockManageContactObject;
        ContactController contactController;
        public ContactTest()
        {
            mockManageContactObject = new Mock<IManageContact>();
            contactController = new ContactController(mockManageContactObject.Object);
            contactController.Request = new HttpRequestMessage();
            contactController.Configuration = new HttpConfiguration();
            contactController.Request.Headers.Add("authenticationToken", "+Bt+PkgskESi2WAEyNhe8Q==");
        }

        #region Functional
        [TestMethod]
        public void GetAllContacts()
        {
            mockManageContactObject.Setup(m => m.GetAllContacts()).Returns(new List<ContactDetail> {
                new ContactDetail
                {
                    ID=1,
                    FirstName="Rakesh",
                    LastName="Patil",
                    Email="Rakesh@gmail.com",
                    PhoneNumber="1234567890",
                    Status=ContactStatus.Active
                } });
            var response = contactController.Get();
            ObjectContent objContent = response.Content as ObjectContent;
            var picklistItem = objContent.Value as List<ContactDetail>;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsTrue(picklistItem.Count == 1);
        }
        [TestMethod]
        public void GetContact()
        {
            mockManageContactObject.Setup(m => m.GetContact(10)).Returns(new ContactDetail
            {
                ID = 1,
                FirstName = "Rakesh",
                LastName = "Patil",
                Email = "Rakesh@gmail.com",
                PhoneNumber = "1234567890",
                Status = ContactStatus.Active
            });

            var response = contactController.Get(10);
            ObjectContent objContent = response.Content as ObjectContent;
            var pickItem = objContent.Value as ContactDetail;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsTrue(pickItem != null);
        }

        [TestMethod]
        public void AddContact_Success()
        {
            mockManageContactObject.Setup(m => m.AddContact(It.IsAny<ContactDetail>())).Returns(1);
            var response = contactController.Post(new ContactDetail
            {
                FirstName = "Rakesh",
                LastName = "Patil",
                Email = "Rakesh@gmail.com",
                PhoneNumber = "1234567890",
                Status = ContactStatus.Active
            });
            ObjectContent objContent = response.Content as ObjectContent;
            var pickItem = objContent.Value as ContactDetail;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created);
            Assert.IsTrue(pickItem.ID == 1);
        }
        [TestMethod]
        public void AddContact_ContactExist()
        {
            mockManageContactObject.Setup(m => m.AddContact(It.IsAny<ContactDetail>())).Returns(0);
            var response = contactController.Post(new ContactDetail
            {
                FirstName = "Rakesh",
                LastName = "Patil",
                Email = "Rakesh@gmail.com",
                PhoneNumber = "1234567890",
                Status = ContactStatus.Active
            });
            ObjectContent objContent = response.Content as ObjectContent;
            var pickItem = objContent.Value as string;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Conflict);
            Assert.IsTrue(pickItem.Equals("Contact Already Exist"));
        }
        [TestMethod]
        public void EditContact_Success()
        {
            mockManageContactObject.Setup(m => m.EditContact(1, It.IsAny<ContactDetail>())).Returns(true);
            var response = contactController.Put(1, new ContactDetail
            {
                ID = 1,
                FirstName = "Rakesh",
                LastName = "Patil",
                Email = "Rakesh@gmail.com",
                PhoneNumber = "1234567890",
                Status = ContactStatus.Active
            });
            ObjectContent objContent = response.Content as ObjectContent;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public void EditContact_ContactExist()
        {
            mockManageContactObject.Setup(m => m.EditContact(1, It.IsAny<ContactDetail>())).Returns(false);
            var response = contactController.Put(1, new ContactDetail
            {
                ID = 1,
                FirstName = "Rakesh",
                LastName = "Patil",
                Email = "Rakesh@gmail.com",
                PhoneNumber = "1234567890",
                Status = ContactStatus.Active
            });
            ObjectContent objContent = response.Content as ObjectContent;
            var pickItem = objContent.Value as string;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotModified);
            Assert.IsTrue(pickItem.Equals("Contact Already Exist with Same Details"));
        }
        [TestMethod]
        public void DeleteContact_Success()
        {
            mockManageContactObject.Setup(m => m.DeleteContact(1)).Returns(true);
            var response = contactController.Delete(1);
            ObjectContent objContent = response.Content as ObjectContent;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public void DeleteContact_ContactNotFound()
        {
            mockManageContactObject.Setup(m => m.DeleteContact(1)).Returns(false);
            var response = contactController.Delete(1);
            ObjectContent objContent = response.Content as ObjectContent;
            var pickItem = objContent.Value as string;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsTrue(pickItem.Equals("Contact not found"));
        }
        #endregion Functional

       
    }
}
