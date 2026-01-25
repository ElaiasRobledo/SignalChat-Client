using Application.Events;
using Application.Interfaces.Auth;
using Application.Interfaces.Chat;
using Application.Interfaces.Contacts;
using Application.Services;
using Presentation.Views.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalChat_Client
{

    public class App
    {
        private readonly IContacts _contacts;
        private readonly IAuth _auth;
        private readonly HttpClient _httpClient;
        private readonly ISignalRClient _signalRClient;
        private readonly ChatService _chatService;
        private readonly UIEventQueue _eventQueue;
        public App(IContacts contacts, HttpClient httpClient, 
            IAuth auth, ISignalRClient signalRClient, 
            ChatService chatService, UIEventQueue eventQueue)
        {
            _contacts = contacts;
            _auth = auth;
            _httpClient = httpClient;
            _chatService = chatService;
            _eventQueue = eventQueue;
            _signalRClient = signalRClient;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                var (username, password) = LoginScreen.Show();
                LoginScreen.ShowStatus();

                var token = await _auth.LoginAsync(username, password);
                await _signalRClient.ConnectAsync(token);
                LoginScreen.ShowFooter();
                Thread.Sleep(1000);

                var router = new Router(token, _contacts, _chatService, _eventQueue);
                await router.RunAsync();
            }
        }
    }

}
