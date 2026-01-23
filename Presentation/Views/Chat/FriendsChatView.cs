using Application.Services;
using Presentation.Views.Utils;
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
            ChatService chatService)
        {
            var friend = context.SelectedFriend;
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[green]Chat with {friend}[/]");
            AnsiConsole.MarkupLine("[grey]Type /exit to return[/]");

            while (true)
            {
                var message = AnsiConsole.Ask<string>("[bold cyan]You[/]:");
                if (string.Equals(message, "/exit", StringComparison.OrdinalIgnoreCase))
                    return Screens.YourFriends;

                if (!string.IsNullOrEmpty(message))
                    await chatService.SendToUserAsync(friend, message);
            }
        }
    }
}