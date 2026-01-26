using Application.Services;
using Presentation.Views.Utils;
using Presentation.Views.Utils.UIs;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Chat
{
    public static class FriendsChatView
    {
        public static async Task<Screens> ShowAsync(
            NavigationContext context,
            ChatService chatService,
            UiDispatcher dispatcher,
            UiState state
            )
        {
            state.CurrentScreen = Screens.FriendsChat;
            state.ActiveChatUser = context.SelectedFriend;

            var friend = context.SelectedFriend;
            var friendId = context.FriendId;

            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[green]Chat with {friend}[/]");
            AnsiConsole.MarkupLine("[grey]Type /exit to return[/]");

            if (state.ChatHistory.TryGetValue(friendId, out var history))
            {
                foreach (var line in history)
                    AnsiConsole.WriteLine(line);
            }


            while (true)
            {
                dispatcher.ProcessEvets();

                if (Console.KeyAvailable || await WaitForInputAsync())
                {
                    AnsiConsole.Write("You: ");

                    var input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input)) continue;

                    if (string.Equals(input, "/exit", StringComparison.OrdinalIgnoreCase))
                    {
                        state.ActiveChatUser = null;
                        return Screens.YourFriends;
                    }

                    if (!state.ChatHistory.TryGetValue(friendId, out var hist))
                    {
                        hist = new List<string>();
                        state.ChatHistory[friendId] = hist;
                    }
                    hist.Add($"You: {input}");

                    await chatService.SendToUserAsync(friendId, input);
                }
                else
                { 
                    await Task.Delay(50);
                }
            }
        }

        private static async Task<bool> WaitForInputAsync()
        {
            await Task.Delay(100);
            return Console.KeyAvailable;

        }

    }
}