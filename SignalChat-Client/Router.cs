using Application.Events;
using Application.Interfaces.Channels;
using Application.Interfaces.Contacts;
using Application.Interfaces.Users;
using Application.Services;
using Presentation.Views.Chat;
using Presentation.Views.Handlers.Channels;
using Presentation.Views.Handlers.Contacts;
using Presentation.Views.Handlers.Friends;
using Presentation.Views.Handlers.Users;
using Presentation.Views.Menus;
using Presentation.Views.Utils;
using Presentation.Views.Utils.UIs;
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
        private readonly UiState _uiState;
        private readonly UiDispatcher _dispatcher;
        private readonly IContacts _contacts;
        private readonly IChannels _channels;
        private readonly ChatService _chatService;
        private readonly UIEventQueue _uiEventQueue;

        private readonly IUsersService _usersService;

        public Router(string token, IContacts contacts,
            ChatService chatService, UIEventQueue eventQueue, 
            IUsersService usersService, IChannels channels)
        {
            _token = token;
            _contacts = contacts;
            _chatService = chatService;
            _uiState = new UiState();
            _usersService = usersService;
            _uiEventQueue = eventQueue;
            _channels = channels;
            _dispatcher = new UiDispatcher(_uiEventQueue, _uiState);
        }

        public async Task RunAsync()
        {
            while(_current != Screens.Exit)
            {
                _uiState.CurrentScreen = _current;

                _current = _current switch
                {
                    //MainMenu
                    Screens.MainMenu => await MainMenu.Show(_uiEventQueue),
                    //Contacts
                    Screens.FriendsMenu => await ContactsMenu.Show(),
                    Screens.YourFriends => await ListContactsHandler.ShowContactsAsync(_token, _contacts, _context),
                    Screens.SearchFriends => await SearchUsersHandler.ShowUserSearch(_token, _usersService, _context),
                    Screens.PendingRequests => await ListPendingFriendsHandler.ShowPendingContacts(_token, _contacts, _context),
                    Screens.SendFriendRequest => await SendFriendRequestHandler.Show(_token, _contacts, _context),
                    Screens.ApprovePendingRequests => await ApprovePendingRequestsHandler.ApproveOrRejectRequest(_token, _contacts, _context),
                    Screens.FriendsChat => await ContactsChatView.ShowAsync(_context, _chatService, _dispatcher, _uiState),
                    Screens.DeleteFriend => await DeleteContactHandler.Delete(_token, _contacts, _context),
                    //Channels
                    Screens.ChannelsMenu => await ChannelsMenu.Show(),
                    Screens.CreateChannel => await CreateChannelHandler.Show(_token,_channels, _context),
                    _ => Screens.Exit
                };
            }
        }
    }
}
