using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Auth
{
    public interface IAuth
    {
        Task<HttpResponseMessage> LoginAsync(string username, string password);
    }
}
