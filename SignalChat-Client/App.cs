using Application.DTOs;
using Application.Events;
using Application.Interfaces.Auth;
using Application.Interfaces.Channels;
using Application.Interfaces.Chat;
using Application.Interfaces.Contacts;
using Application.Interfaces.Users;
using Application.Services;
using Presentation.Views.Menus;
using Presentation.Views.Utils;
using Presentation.Views.Utils.UIs;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SignalChat_Client
{

    public class App
    {
        private readonly IContacts _contacts;
        private readonly IAuth _auth;
        private readonly HttpClient _httpClient;
        private readonly ISignalRClient _signalRClient;
        private readonly ChatService _chatService;
        private readonly IChannels _channels;
        private readonly UiState _state;
        private readonly IUsersService _usersService;
        private readonly UIEventQueue _eventQueue;
        public App(IContacts contacts, HttpClient httpClient, 
            IAuth auth, ISignalRClient signalRClient, 
            ChatService chatService, UIEventQueue eventQueue,
            IUsersService usersService,
            IChannels channels, UiState uistate)
        {
            _contacts = contacts;
            _auth = auth;
            _state = uistate;
            _httpClient = httpClient;
            _chatService = chatService;
            _eventQueue = eventQueue;
            _channels = channels;
            _usersService = usersService;
            _signalRClient = signalRClient;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                var (username, password) = LoginScreen.Show();
                LoginScreen.ShowStatus();

                var response = await _auth.LoginAsync(username, password);

                if (!response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var error = JsonSerializer.Deserialize<ApiError>(jsonResponse);
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine($"[bold red]{Markup.Escape(error?.Message ?? "Error desconocido")}[/]");
                    AnsiConsole.MarkupLine("\n[grey]Press any key to try again...[/]");

                    Console.ReadKey(true);

                    Console.Clear();
                    continue;
                }
                var token = await response.Content.ReadAsStringAsync();
                
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var userIdClaim = jwt.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.NameIdentifier ||
                    c.Type == "nameid"
                );
                var userId = Guid.Parse(userIdClaim!.Value);
                _state.ActiverUserId = userId;

                await _signalRClient.ConnectAsync(token);
                Console.WriteLine(userIdClaim!.Value);
                LoginScreen.ShowFooter();
                Thread.Sleep(1000);

                
                var router = new Router(token, _contacts, 
                _chatService, _eventQueue, 
                _usersService, _channels,_state);
                await router.RunAsync();
            }
        }
    }
    }

