using Application.Interfaces.Contacts;
using Application.Interfaces.Users;
using Presentation.Views.Decorations;
using Presentation.Views.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Handlers.Contacts
{
    public static class SendFriendRequestHandler
    {
        public static async Task<Screens> Show(
            string token,
            IContacts contactsService,
            NavigationContext context)
        {
            if (context.UserId == null || context.Username == null)
                return Screens.FriendsMenu;

            Helpers helpers = new Helpers();
            helpers.DrawHeader();

            AnsiConsole.MarkupLine("[green]User selected:[/]");
            AnsiConsole.MarkupLine($"• Username: [bold]{context.Username}[/]");
            AnsiConsole.WriteLine();

            var confirm = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(new Style(Color.Green1))
                    .AddChoices("Yes", "No")
                    .Title($"Do you want to send a friend request to [bold]{context.Username}[/]?"));

            if (confirm == "Yes")
            {
                var result = await contactsService.SendContactRequestAsync(
                    token,
                    context.Username);

                if (result.IsSuccessStatusCode)
                {
                    AnsiConsole.MarkupLine("[green]Friend request sent successfully.[/]");
                    AnsiConsole.MarkupLine("\n[grey]Press any key to continue...[/]");
                    Console.ReadKey(true);
                }
                else
                {
                    var error = await result.Content.ReadAsStringAsync();
                    AnsiConsole.MarkupLine($"[red]{Markup.Escape(error)}[/]");
                    AnsiConsole.MarkupLine("\n[grey]Press any key to continue...[/]");
                    Console.ReadKey(true);
                }

            }

            context.UserId = null;
            context.Username = null;

            return Screens.FriendsMenu;
        }
    }


}
