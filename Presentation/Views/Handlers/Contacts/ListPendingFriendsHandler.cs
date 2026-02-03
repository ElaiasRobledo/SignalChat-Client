using Application.DTOs;
using Application.Interfaces.Contacts;
using Presentation.Views.Decorations;
using Presentation.Views.Menus;
using Presentation.Views.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Presentation.Views.Handlers.Friends
{
    public static class ListPendingFriendsHandler
    {
        public static async Task<Screens> ShowPendingContacts(
            string token, IContacts contacts, NavigationContext context)
        {
            AnsiConsole.Clear();
            Banner.Show();
            AnsiConsole.MarkupLine("[green1]Your contact request:[/]");

            var response = await contacts.GetPendingContactsAsync(token);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<GetFriendsDto>>(content);

            if (result == null || result.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red] You dont have any request yet[/][yellow]:sad_but_relieved_face:[/]");

                AnsiConsole.MarkupLine("\n[grey]Press any key to go back...[/]");
                Console.ReadKey(true);
                return Screens.FriendsMenu;

            }

            const string BackOption = "Back to Friend's Menu";
            var contactSelection = AnsiConsole.Prompt(
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

            if (contactSelection == BackOption)
                return Screens.FriendsMenu;

            var selectedUser = result.First(u => u.username == contactSelection);

            context.Username = selectedUser.username;
            context.UserId = selectedUser.userId;

            LoginScreen.ShowStatus();


            return Screens.ApprovePendingRequests;

        }
    }
}
