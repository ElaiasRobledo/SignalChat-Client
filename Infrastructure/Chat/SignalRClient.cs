using Application.Interfaces.Chat;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Chat
{
    public class SignalRClient : ISignalRClient
    {
        private HubConnection _connection;

        //READ ABOUT EVENTS AND PRIORITIZE THE OOP.
        public event Action<string, string> OnReceiveMessage;

        public async Task ConnectAsync(string token)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7139/chat", 
                option =>
                {
                    option.AccessTokenProvider = () => Task.FromResult(token);
                })
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string, string>("ReceiveMessage", (user, message)
                =>
            {
                OnReceiveMessage?.Invoke(user, message);

            });
            await _connection.StartAsync();


        }
        public Task SendToUserAsync(string username, string msg)
            => _connection.InvokeAsync("SendMsgToSpecificUser", username, msg);
    }
}
