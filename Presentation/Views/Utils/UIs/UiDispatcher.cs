using Application.Events;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Utils.UIs
{
    public sealed class UiDispatcher
    {
        private readonly UIEventQueue _events;
        private readonly UiState _state;

        public UiDispatcher(UIEventQueue events,
            UiState state)
        {
            _events = events;
            _state = state;
        }

        public void ProcessEvets()
        {
            while (_events.TryDequeue(out var ev))
            {
                if (ev is IncomingChatMessage msg)
                    HandleChat(msg);
            }
        }
       public void ProccesGroupsEvents()
        {
            while (_events.TryDequeue(out var ev))
            {
                if(ev is IncomingGroupChatMesages msg)
                    HandleGroupChat(msg);
            }
        }

        //1-1
        private void HandleChat(IncomingChatMessage msg)
        {

            if (!_state.ChatHistory.TryGetValue(msg.FromUser, out var history))
            {
                history = new List<string>();
                _state.ChatHistory[msg.FromUser] = history;
            }

            var line = $"{msg.FromUser}: {msg.Message}";
            history.Add(line);

            if (_state.CurrentScreen == Screens.FriendsChat &&
        _state.ActiveChatUser == msg.FromUser)
            {
                AnsiConsole.WriteLine(line);
            }
          
        }
        
        //n-n
        private void HandleGroupChat(IncomingGroupChatMesages msg)
        {
                
                var ChannelId = msg.ChannelId;
                if(!_state.ChatHistory.TryGetValue(ChannelId, out var chatHistory))
                {
                    chatHistory = new List<string>();
                    _state.ChatHistory[ChannelId] = chatHistory;

                }
                if(_state.ActiverUserId != msg.FromUserId)
                {
                    var msgline = $"{msg.FromUser}: {msg.Message}";        
                    
                    chatHistory.Add(msgline);
                    if (_state.CurrentScreen == Screens.ChannelsChat &&
                        _state.ActiveChannelId == ChannelId)
                    {
                        AnsiConsole.WriteLine(msgline);
                    }
                }
        }
    }
}