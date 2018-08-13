using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDataBase
{
    public class DataProvider : IDataProvider
    {
        static List<ContactDetail> contactList;
        static DataProvider()
        {
            contactList = new List<ContactDetail> {
                new ContactDetail
                {
                    ID=1,
                    FirstName="Rakesh",
                    LastName="Patil",
                    Email="Rakesh@gmail.com",
                    PhoneNumber="1234567890",
                    Status=ContactStatus.Active
                },
                 new ContactDetail
                {
                    ID=2,
                    FirstName="Amar",
                    LastName="Patil",
                    Email="Amar@gmail.com",
                    PhoneNumber="2343455768",
                    Status=ContactStatus.Active
                },
                  new ContactDetail
                {
                    ID=3,
                    FirstName="Umesh",
                    LastName="Patil",
                    Email="Umesh@gmail.com",
                    PhoneNumber="6564879545",
                    Status=ContactStatus.Active
                },
                   new ContactDetail
                {
                    ID=4,
                    FirstName="Ram",
                    LastName="Patil",
                    Email="Ram@gmail.com",
                    PhoneNumber="2324354657",
                    Status=ContactStatus.Active
                }
            };
        }
        #region PrivateMethods
        private bool IsExist(ContactDetail contactDetail, int expectedCount = 0)
        {
            var count = contactList.Where(x =>
                                    x.Email.Equals(contactDetail.Email, StringComparison.InvariantCultureIgnoreCase) ||
                                    x.PhoneNumber.Equals(contactDetail.PhoneNumber, StringComparison.InvariantCultureIgnoreCase) ||
                                    x.FirstName.Equals(contactDetail.FirstName, StringComparison.InvariantCultureIgnoreCase) ||
                                    x.LastName.Equals(contactDetail.LastName, StringComparison.InvariantCultureIgnoreCase)).Count();

            return (count == expectedCount) ? false : true;
        }
        #endregion PrivateMethods
        public int AddContact(ContactDetail contactDetail)
        {
            if (IsExist(contactDetail))
            {
                return 0;
            }
            contactDetail.ID = contactList.Max(x => x.ID) + 1;
            contactList.Add(contactDetail);
            return contactDetail.ID;
        }
        public bool EditContact(int id, ContactDetail contactDetail)
        {
            contactList.RemoveAll(x => x.ID == id);
            contactDetail.ID = id;
            contactList.Add(contactDetail);
            return true;
        }
        public bool DeleteContact(int id)
        {
            var temp = contactList.Where(x => x.ID == id).FirstOrDefault();
            contactList.RemoveAll(x => x.ID == id);
            temp.Status = ContactStatus.Inactive;
            contactList.Add(temp);
            return true;
        }
        public List<ContactDetail> GetContactList()
        {
            return contactList;
        }
    }
}
