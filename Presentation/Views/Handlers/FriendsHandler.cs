using Application.Interfaces.Contacts;
using Presentation.Views.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Handlers
{
    public static class FriendsHandler
    {
        public static async Task<Screens> ShowFriendsAsync(
            string token, IContacts contacts)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green]Your friends:[/]");

            var response = await contacts.GetContactsAsync(token);
            var content = await response.Content.ReadAsStringAsync();

            AnsiConsole.WriteLine(content);

            AnsiConsole.MarkupLine("\n[grey]Press any key to go back...[/]");
            Console.ReadKey(true);

            return Screens.FriendsMenu;
        }

    }
}
