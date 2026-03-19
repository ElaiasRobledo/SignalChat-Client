
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
            _chatClient.OnReceiveGroupMessage += HandleGroupChatIncomingMessages;
        }

        private void HandleIncomingMessage(string user, string message)
        {
            _eventQueue.Enqueue(
                new IncomingChatMessage(user, message));

        }
        private void HandleGroupChatIncomingMessages(string channelId,Guid userId ,string user, string message)
        {
         _eventQueue.Enqueue
                (new IncomingGroupChatMesages(userId,channelId ,user, message));
        }
        public Task ConnectAsync(string token)
            => _chatClient.ConnectAsync(token);
        public Task SendToUserAsync(string user, string message)
            => _chatClient.SendToUserAsync(user, message);

        public Task SendToGroupAsync(string channelId, string msg)
            => _chatClient.SendToChannelAsync(channelId, msg);

    }
}
