
using Application.Events;
using Application.Interfaces.Chat;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ChatService
    {
        private readonly ISignalRClient _chatClient;
        private readonly UIEventQueue _eventQueue;
        public ChatService(ISignalRClient chatClient, UIEventQueue events)
        {
            _chatClient = chatClient;
            _eventQueue = events;
            _chatClient.OnReceiveMessage += HandleIncomingMessage; //ENTENDER COMO FUNCIONA ESTO
        }

        private void HandleIncomingMessage(string user, string message)
        {
            _eventQueue.Enqueue(
                new IncomingChatMessage(user, message));

        }
        public Task ConnectAsync(string token)
            => _chatClient.ConnectAsync(token);
        public Task SendToUserAsync(string user, string message)
            => _chatClient.SendToUserAsync(user, message);
    }
}
