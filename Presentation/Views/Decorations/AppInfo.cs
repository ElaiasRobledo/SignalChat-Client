using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Decorations
{
    public static class AppInfo
    {
        public static void Show()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine(
                "[green]   Information about the app, version, creators, repo link...[/]");
            AnsiConsole.WriteLine();
        }
    }

}
