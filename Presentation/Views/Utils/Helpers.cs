using Presentation.Views.Decorations;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Utils
{
    public class Helpers
    {

        public void DrawHeader()
        {
            AnsiConsole.Clear();
            Banner.Show();
            AnsiConsole.WriteLine();
        }

    }
}
