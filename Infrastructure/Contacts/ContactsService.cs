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

            var response = await _httpClient.GetAsync("approved");
            return response;

        }

        public async Task<HttpResponseMessage> GetPendingContactsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("pending");
            return response;

        }
        public async Task<HttpResponseMessage> ApproveContactsAsync(string token, string userId)
        {
            _httpClient.DefaultRequestHeaders.Authorization
               = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsync($"approve/{userId}",null);
            return response;
        }
        public async Task<HttpResponseMessage> RejectContactsAsync(string token, string userId)
        {
            _httpClient.DefaultRequestHeaders.Authorization
               = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsync($"reject/{userId}", null);
            return response;
        }
    }
}
