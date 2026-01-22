using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Decorations
{
    public static class Banner
    {
        public static void Show()
        {
            var font = FigletFont.Load("fonts/Epic.flf");

            AnsiConsole.Write(
                new FigletText(font, "SignalChat")
                    .Centered()
                    .Color(Color.Green1));
        }
    }

}
