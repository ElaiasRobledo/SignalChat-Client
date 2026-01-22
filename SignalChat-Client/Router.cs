using Application.Interfaces.Contacts;
using Presentation.Views.Handlers;
using Presentation.Views.Menus;
using Presentation.Views.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalChat_Client
{
    public class Router
    {
        private Screens _current = Screens.MainMenu;
        private readonly string _token;
        private readonly IContacts _contacts;

        public Router(string token, IContacts contacts)
        {
            _token = token;
            _contacts = contacts;
        }

        public async Task RunAsync()
        {
            while(_current != Screens.Exit)
            {
                _current = _current switch
                {
                    Screens.MainMenu => await MainMenu.Show(),
                    Screens.FriendsMenu => await FriendsMenu.Show(),
                    Screens.YourFriends => await FriendsHandler.ShowFriendsAsync(_token, _contacts),
                    _ => Screens.Exit
                };
            }
        }
    }
}
