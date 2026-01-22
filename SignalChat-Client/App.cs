using Application.Interfaces.Auth;
using Application.Interfaces.Contacts;
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
        public App(IContacts contacts, HttpClient httpClient, IAuth auth)
        {
            _contacts = contacts;
            _auth = auth;
            _httpClient = httpClient;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                var (username, password) = LoginScreen.Show();
                LoginScreen.ShowStatus();

                var token = await _auth.LoginAsync(username, password);

                LoginScreen.ShowFooter();
                Thread.Sleep(1000);

                var router = new Router(token, _contacts);
                await router.RunAsync();
            }
        }
    }

}
