using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Users
{
    public interface IUsersService
    {
        Task<HttpResponseMessage> GetAsync(string token,string username);
    }
}
