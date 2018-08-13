using Entities;
using Factory;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ManageContact: IManageContact
    {
        private IDataProvider _dataProvider;
        public ManageContact()
        {
            DataProviderFactory dataProviderFactory = new DataProviderFactory();
             _dataProvider = dataProviderFactory.CreateDataProvider();
        }
        public List<ContactDetail> GetAllContacts()
        {
            return _dataProvider.GetContactList();
        }
        public ContactDetail GetContact(int id)
        {
            return _dataProvider.GetContactList().Where(x => x.ID == id).FirstOrDefault();
        }
        public int AddContact(ContactDetail contact)
        {
            return _dataProvider.AddContact(contact);
        }
        public bool EditContact(int id,ContactDetail contact)
        {
            return _dataProvider.EditContact(id,contact);
        }
        public bool DeleteContact(int id)
        {
            return _dataProvider.DeleteContact(id);
        }
    }
}
