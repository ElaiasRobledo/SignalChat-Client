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

        public SignalRClient()
        {

        }

        //READ ABOUT EVENTS AND PRIORITIZE THE OOP.
        public event Action<string, string> OnReceiveMessage;
        public event Action<string,Guid, string,string> OnReceiveGroupMessage;

        public async Task ConnectAsync(string token)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5262/chat", 
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
            _connection.On<string,Guid,string,string>("ReceiveGroupMessages", (channelId,userId,user, message) 
                =>
            {
                OnReceiveGroupMessage?.Invoke(channelId,userId,user, message);
            });
            
            await _connection.StartAsync();


        }
        public Task SendToChannelAsync(string channeld, string msg)
            => _connection.InvokeAsync("SendMessageToGroup", channeld, msg);
        
        public Task SendToUserAsync(string username, string msg)
            => _connection.InvokeAsync("SendMessageToSpecificClient", username, msg);
    }
}
