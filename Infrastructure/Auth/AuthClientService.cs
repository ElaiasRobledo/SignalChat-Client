using Application.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Auth
{
    public class AuthClientService : IAuth
    {
        private readonly HttpClient _http;

        public AuthClientService(HttpClient http)
        { _http = http; }

        public async Task<HttpResponseMessage> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Data is required");

            var payload = new { username, password };

            var content = new StringContent(JsonSerializer.Serialize(payload),
               Encoding.UTF8,
                "application/json");

            var response = await _http.PostAsync
                ("login", content);

            var token = await response.Content.ReadAsStringAsync();

            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return response;
        }
    }
}