
using Application.Interfaces.Chat;
using Application.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ChatService
    {
        private readonly ISignalRClient _chatClient;
        private readonly ChatBuffer _chatBuffer;
        public ChatService(ISignalRClient chatClient, ChatBuffer chatBuffer)
        {
            _chatClient = chatClient;
            _chatBuffer = chatBuffer;
            _chatClient.OnReceiveMessage += HandleIncomingMessage; //ENTENDER COMO FUNCIONA ESTO
        }

        private void HandleIncomingMessage(string user, string message)
        {
            _chatBuffer.Add(user, message);        
        }
        public Task ConnectAsync(string token)
            => _chatClient.ConnectAsync(token);
        public Task SendToUserAsync(string user, string message)
            => _chatClient.SendToUserAsync(user, message);
    }
}
