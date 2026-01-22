using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Contacts
{
    public interface IContacts
    {
        Task<HttpResponseMessage> GetContactsAsync(string token);

    }
}
