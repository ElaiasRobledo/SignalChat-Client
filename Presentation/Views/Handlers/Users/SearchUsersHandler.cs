using Application.DTOs;
using Application.Interfaces.Users;
using Presentation.Views.Decorations;
using Presentation.Views.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Presentation.Views.Handlers.Users
{
    public static class SearchUsersHandler
    {
        private const string BackOption = "Back to Friend's Menu";
        private const string NewSearchOption = "New Search";
        private const string InputBackOption = "/exit";
        public static async Task<Screens> ShowUserSearch
            (
            string token, 
            IUsersService usersService,
            NavigationContext navigationContext
            )
        {
            Helpers helpers = new();
            helpers.DrawHeader();

            AnsiConsole.Markup("[grey]Type /exit to return[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[green1]Search your new friend: [/]");

            var username = ReadUsername();
            if (string.IsNullOrWhiteSpace(username))
                return Retry("Please enter a value");

            if (username == InputBackOption)
                return Screens.FriendsMenu;

            var users = await SearchUsers(token, usersService, username);
            if (users.Count == 0)
                return Retry("No users were found");

            var selection = AskUserSelection(users);
            return ResolveSelection(selection, users, navigationContext);
        }
     
        private static string? ReadUsername()
        {
            return Console.ReadLine();
        }
        private static async Task<List<GetFriendsDto>> SearchUsers(
       string token,
       IUsersService service,
       string username)
        {
            var response = await service.GetAsync(token, username);
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<GetFriendsDto>>(content)
                   ?? new List<GetFriendsDto>();
        }

        private static string AskUserSelection(List<GetFriendsDto> users)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(new Style(Color.Green3_1))
                    .PageSize(10)
                    .AddChoices(users.Select(u => u.username))
                    .AddChoices(NewSearchOption, BackOption)
                      .UseConverter(choice =>
                      {
                          if (choice == NewSearchOption)
                              return "[bold white]New Search[/]";
                          if (choice == BackOption)
                              return "[bold red]Back to Friend's Menu[/]";

                          return choice;
                      }));
        }

        private static Screens ResolveSelection(string selection,
            List<GetFriendsDto> users,
    NavigationContext context)
        {
            if (selection == NewSearchOption)
                return Screens.SearchFriends;

            if(selection == InputBackOption)
                return Screens.FriendsMenu;

            if (selection == BackOption)
                return Screens.FriendsMenu;

            var selectedUser = users.First(u => u.username == selection);

            context.UserId = selectedUser.userId;
            context.Username = selectedUser.username;

            return Screens.SendFriendRequest;

        }

        private static Screens Retry(string message)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[bold red]{message}[/]");
            AnsiConsole.MarkupLine("\n[grey]Press any key to continue...[/]");
            Console.ReadKey(true);

            return Screens.SearchFriends;
        }
    }
}
