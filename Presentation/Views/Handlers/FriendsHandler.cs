using Application.DTOs;
using Application.Interfaces.Contacts;
using Presentation.Views.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Presentation.Views.Handlers
{
    public static class FriendsHandler
    {
        public static async Task<Screens> ShowFriendsAsync(
            string token, IContacts contacts, NavigationContext context)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green]Your friends:[/]");

            var response = await contacts.GetContactsAsync(token);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<GetFriendsDto>>(content);

            if (result == null || result.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red] You dont have any friends yet[/][yellow]:sad_but_relieved_face:[/]");

                AnsiConsole.MarkupLine("\n[grey]Press any key to go back...[/]");
                Console.ReadKey(true);
                return Screens.FriendsMenu;
            }

            var selectedUsername = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Selecciona un amigo[/]")
                .HighlightStyle(new Style(Color.Green1))
                .PageSize(10)
                .AddChoices(result.Select(f => f.username))
                );

            var selectedFriend = result.First(f => f.username == selectedUsername);
            context.SelectedFriend = selectedFriend.userId;

            return Screens.FriendsChat;
        }
    }
}