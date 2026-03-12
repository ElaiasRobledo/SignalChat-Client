using Application.Events;
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
        public static Task<Screens> Show(UIEventQueue eventQueue)
        {
            var incomingMessages = eventQueue.CountIncomingMessages();
            AnsiConsole.Clear();
            Banner.Show();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(new Style(Color.Green1))
                    .AddChoices(
                        "Friends",
                        "Channels",
                        $"Messages [bold SpringGreen1]({incomingMessages})[/]",
                        "Settings",
                        "Exit"));

            return Task.FromResult(
                
                option switch
                {
                    "Friends" => Screens.FriendsMenu,
                    "Channels" => Screens.ChannelsMenu,
                    "Exit" => Screens.Exit,
                    _ => Screens.MainMenu
                }
                
                );
        }
    }


}


