using Application.Interfaces.Contacts;
using Application.Services;
using Presentation.Views.Chat;
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
        private readonly NavigationContext _context = new();
        private readonly string _token;
        private readonly IContacts _contacts;
        private readonly ChatService _chatService;

        public Router(string token, IContacts contacts, ChatService chatService)
        {
            _token = token;
            _contacts = contacts;
            _chatService = chatService;
        }

        public async Task RunAsync()
        {
            while(_current != Screens.Exit)
            {
                _current = _current switch
                {
                    Screens.MainMenu => await MainMenu.Show(),
                    Screens.FriendsMenu => await FriendsMenu.Show(),
                    Screens.YourFriends => await FriendsHandler.ShowFriendsAsync(_token, _contacts, _context),
                    Screens.FriendsChat => await FriendsChatView.ShowAsync(_context, _chatService),
                    _ => Screens.Exit
                };
            }
        }
    }
}
