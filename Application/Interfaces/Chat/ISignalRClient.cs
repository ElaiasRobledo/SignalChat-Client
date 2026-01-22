using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Chat
{
    public interface ISignalRClient
    {
        event Action<string, string> OnReceiveMessage;

        Task ConnectAsync(string token);
        Task SendToUserAsync(string username, string msg);

    }
}
