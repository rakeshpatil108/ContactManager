using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDataProvider
    {
        List<ContactDetail> GetContactList();
        bool EditContact(int id,ContactDetail contactDetail);
        int AddContact(ContactDetail contactDetail);
        bool DeleteContact(int id);
    }
}
