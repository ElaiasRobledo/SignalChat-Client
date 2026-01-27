using Application.DTOs;
using Application.Interfaces.Contacts;
using Presentation.Views.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Presentation.Views.Handlers.Friends
{
    public static class ListContactsHandler
    {
        public static async Task<Screens> ShowContactsAsync(
            string token, IContacts contacts, NavigationContext context)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green1]Your friends:[/]");

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
            const string BackOption = "Back to Friend's Menu";
            var selectedUsername = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .HighlightStyle(new Style(Color.Green1))
                .PageSize(10)
                .AddChoices(result.Select(f => f.username))
                .AddChoices(BackOption)
                .UseConverter(choice =>
                choice == BackOption
                 ? "[bold red]Back to Friend's Menu[/]"
                : choice
                ));

            if (selectedUsername == BackOption)
                return Screens.FriendsMenu;


            var selectedFriend = result.First(f => f.username == selectedUsername);
            context.Username = selectedFriend.username;
            context.UserId = selectedFriend.userId;

            return Screens.FriendsChat;
        }
    }
}