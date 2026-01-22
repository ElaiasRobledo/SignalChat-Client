using Presentation.Views.Decorations;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Menus
{
    public static class LoginScreen
    {
        public static (string Username, string Password) Show()
        {
            AnsiConsole.Clear();

            Banner.Show();
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine();

            var username = AnsiConsole.Ask<string>("[black]_____[/][Green1]Username:[/]");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("[black]_____[/][Green1]Password:[/]")
                    .Secret());

            return (username, password);
        }

        public static void ShowStatus()
        {
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .SpinnerStyle(new Style(Color.Green))
                .Start("[green]Logging in...[/]", _ =>
                {
                    Thread.Sleep(1200);
                });
        }

        public static void ShowFooter()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine(
                "[green]Information about the app, version, creators, repository link, etc.[/]");
        }
    }
}