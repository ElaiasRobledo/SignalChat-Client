using Presentation.Views.Decorations;
using Presentation.Views.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Menus
{
    public static class MainMenu
    {
        public static Task<Screens> Show()
        {
            AnsiConsole.Clear();
            Banner.Show();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(new Style(Color.Green1))
                    .AddChoices(
                        "Friends",
                        "Groups",
                        "Messages",
                        "Settings",
                        "Exit"));

            return Task.FromResult(
                
                option switch
                {
                    "Friends" => Screens.FriendsMenu,
                    "Exit" => Screens.Exit,
                    _ => Screens.MainMenu
                }
                
                );
        }
    }


}


