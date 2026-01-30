using Application.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Users
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        public UsersService(HttpClient httpClient)
        { 
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> GetAsync(string token, string username)
        {
            _httpClient.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(
              $"users?username={Uri.EscapeDataString(username)}");

            return response;
        }
      
    }
}
