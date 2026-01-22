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

        public async Task<string> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Data is required");

            var payload = new {username, password};

            var content = new StringContent(JsonSerializer.Serialize(payload),
               Encoding.UTF8,
                "application/json");

            //DESPUES INVOCAMOS LA URL DESDE APPSETTING U OTRO ARCHIVO.
            var response = await _http.PostAsync
                ("https://localhost:7139/api/auth/login", content);

            var token = (await response.Content.ReadAsStringAsync()).Trim();

            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return token;
        }
    }
}
