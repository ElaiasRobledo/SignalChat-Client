
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
        public ChatService(ISignalRClient chatClient)
        {
            _chatClient = chatClient;
            _chatClient.OnReceiveMessage += HandleIncomingMessage; //ENTENDER COMO FUNCIONA ESTO
        }

        private void HandleIncomingMessage(string user, string message)
        { 
            AnsiConsole.MarkupLine($"[bold green]{user}[/]: {Markup.Escape(message)}");
        }
        public Task ConnectAsync(string token)
            => _chatClient.ConnectAsync(token);
        public Task SendToUserAsync(string user, string message)
            => _chatClient.SendToUserAsync(user, message);
    }
}
