using Application.DTOs;
using Application.Interfaces.Contacts;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Presentation.Views.Handlers.Selectors.Contacts
{
    public static class ContactSelector
    {
        public static GetFriendsDto? Select(
        IReadOnlyList<GetFriendsDto> friends)
        {
            if (friends.Count == 0)
                return null;

            const string BackOption = "[bold red]Back to Friend's Menu[/]";

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(new Style(Color.Green1))
                    .PageSize(10)
                    .AddChoices(friends.Select(f => f.username))
                    .AddChoices(BackOption)
                    .UseConverter(c =>
                        c == BackOption
                            ? "[bold red]Back to Friend's Menu[/]"
                            : c));

            if (choice == BackOption)
                return null;

            return friends.First(f => f.username == choice);
        }

        public static void RenderFriendsList(IEnumerable<GetFriendsDto> friends)
        {
            AnsiConsole.MarkupLine("[green1]Your friends:[/]");

            foreach (var friend in friends)
            {
                AnsiConsole.MarkupLine($" • [white]{friend.username}[/]");
            }

            AnsiConsole.WriteLine();
        }
    }

}
