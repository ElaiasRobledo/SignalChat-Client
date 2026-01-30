using Application.Interfaces.Contacts;
using Presentation.Views.Decorations;
using Presentation.Views.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Menus
{
    public static class ContactsMenu
    {
        public static Task<Screens> Show()
        {
            AnsiConsole.Clear();
            Banner.Show();


            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(new Style(Color.Green))
                    .AddChoices(
                        "Your friends",
                        "Delete a friend",
                        "Search new friends",
                        "Pending requests (n)", 
                        "Back to menu"));

            return Task.FromResult(

                option switch
                {
                    "Your friends" => Screens.YourFriends,
                    "Pending requests (n)" => Screens.PendingRequests, //Redirigir a otra screen para aceptar o rechazar
                    "Search new friends" => Screens.SearchFriends,
                    "Back to menu" => Screens.MainMenu,
                    _ => Screens.FriendsMenu
                });
        }
    }
}