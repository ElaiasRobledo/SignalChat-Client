using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Contacts
{
    public interface IContacts
    {
        Task<HttpResponseMessage> GetContactsAsync(string token);
        Task<HttpResponseMessage> GetPendingContactsAsync(string token);
        Task<HttpResponseMessage> ApproveContactsAsync(string token, string userId);
        Task<HttpResponseMessage> RejectContactsAsync(string token, string userId);
        Task<HttpResponseMessage> SendContactRequestAsync(string token, string Username);
        Task DeleteAsync(string token, string contactId);
    }
}
