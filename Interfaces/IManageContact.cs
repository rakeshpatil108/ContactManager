using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IManageContact
    {
         List<ContactDetail> GetAllContacts();
         ContactDetail GetContact(int id);
         int AddContact(ContactDetail contact);
         bool EditContact(int id, ContactDetail contact);
         bool DeleteContact(int id);
    }
}
