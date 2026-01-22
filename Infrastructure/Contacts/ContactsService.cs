using Application.Interfaces.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Contacts
{
    public class ContactsService : IContacts
    {
        private readonly HttpClient _httpClient;
        public ContactsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetContactsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("https://localhost:7139/api/contacts/approved");
            return response;

        }
    }
}
