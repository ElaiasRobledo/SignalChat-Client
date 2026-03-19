using Presentation.Views.Decorations;
using Presentation.Views.Utils;
using Spectre.Console;

namespace Presentation.Views.Menus
{
    public static class ChannelsMenu
    {
        public static Task<Screens> Show()
        {
            AnsiConsole.Clear();
            Banner.Show();

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .HighlightStyle(new Style(Color.Green1))
                .AddChoices
                ("My channels",
                "Create a channel",
                "Back to menu"
                )
            );

            return Task.FromResult
            (
                option switch
                {
                    "My channels" => Screens.MyChannels,
                    "Create a channel" => Screens.CreateChannel,
                    "Back to menu" => Screens.MainMenu,
                    _ => Screens.MainMenu,

                }

            );
        }
    }

}