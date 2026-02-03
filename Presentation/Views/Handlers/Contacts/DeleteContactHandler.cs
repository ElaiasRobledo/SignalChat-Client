using Application.DTOs;
using Application.Interfaces.Contacts;
using Presentation.Views.Decorations;
using Presentation.Views.Handlers.Selectors.Contacts;
using Presentation.Views.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Presentation.Views.Handlers.Contacts
{
    public static class DeleteContactHandler
    {
        public static async Task<Screens> Delete(
            string token,
            IContacts contacts,
            NavigationContext context)
        {
            AnsiConsole.Clear();
            Banner.Show();

            AnsiConsole.MarkupLine("[red]Delete a friend[/]");


            var response = await contacts.GetContactsAsync(token);
            var content = await response.Content.ReadAsStringAsync();
            var friends = JsonSerializer.Deserialize<List<GetFriendsDto>>(content)
                          ?? new();

            if (friends == null || friends.Count == 0)
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
                .AddChoices(friends.Select(f => f.username))
                .AddChoices(BackOption)
                .UseConverter(choice =>
                choice == BackOption
                 ? "[bold red]Back to Friend's Menu[/]"
                : choice
                ));

            if (selectedUsername == BackOption)
                return Screens.FriendsMenu;

            var selectedFriend = friends.First(f => f.username == selectedUsername);
            context.Username = selectedFriend.username;
            context.UserId = selectedFriend.userId;
            var confirm = AnsiConsole.Confirm(
                $"Are you sure you want to delete [red]{selectedFriend.username}[/]?");

            if (!confirm)
                return Screens.FriendsMenu;

            await contacts.DeleteAsync(token, selectedFriend.userId.ToString());

            AnsiConsole.MarkupLine("[green]Friend deleted successfully.[/]");
            Thread.Sleep(1000);

            return Screens.FriendsMenu;
        }
    }
}