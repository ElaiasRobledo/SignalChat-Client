using System.Text.Json;
using Application.Interfaces.Channels;
using Presentation.Views.Utils;
using Spectre.Console;

namespace Presentation.Views.Handlers.Channels
{
    public static class SearchChannelsHandler
    {
        private const string BackOption = "Back to Channels Menu";
        private const string NewSearchOption = "New Search";
        private const string InputBackOption = "/exit";

        public static async Task<Screens> ShowChannelSearch
        (
            string token,
            IChannels channelService,
            NavigationContext navigationContext
        )
        {
            Helpers helpers = new();
            helpers.DrawHeader();

            AnsiConsole.Markup("[grey]Type /exit to return[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[green1]Search channel: [/]");

            var channelName = ReadChannelName();

            if (string.IsNullOrWhiteSpace(channelName))
                return Retry("Please enter a value");

            if (channelName == InputBackOption)
                return Screens.ChannelsMenu;

            var channels = await SearchChannels(token, channelService, channelName);

            if (channels.Count == 0 || !channels.Any())
                return Retry("No channels were found");

            var selection = AskChannelSelection(channels);

            return ResolveSelection(selection, channels, navigationContext);
        }

        private static string? ReadChannelName()
        {
            return Console.ReadLine();
        }

        private static async Task<List<MyChannelsDto>> SearchChannels(
            string token,
            IChannels service,
            string channelName)
        {
            var response = await service.SearchByNameAsync(token, channelName);
            var content = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<List<MyChannelsDto>>(content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<MyChannelsDto>();
        }

        private static string AskChannelSelection(List<MyChannelsDto> channels)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(new Style(Color.Green3_1))
                    .PageSize(10)
                    .AddChoices(channels.Select(c => c.name))
                    .AddChoices(NewSearchOption, BackOption)
                    .UseConverter(choice =>
                    {
                        if (choice == NewSearchOption)
                            return "[bold white]New Search[/]";
                        if (choice == BackOption)
                            return "[bold red]Back to Channels Menu[/]";

                        return choice;
                    }));
        }

        private static Screens ResolveSelection(
            string selection,
            List<MyChannelsDto> channels,
            NavigationContext context)
        {
            if (selection == NewSearchOption)
                return Screens.SearchChannels;

            if (selection == BackOption)
                return Screens.ChannelsMenu;

            var selectedChannel = channels.First(c => c.name == selection);

            context.ChannelId = selectedChannel.id;
            context.ChannelName = selectedChannel.name;

            return Screens.ChannelDetails; // o JoinChannel / ViewChannel según tu flujo
        }

        private static Screens Retry(string message)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[bold red]{message}[/]");
            AnsiConsole.MarkupLine("\n[grey]Press any key to continue...[/]");
            Console.ReadKey(true);

            return Screens.SearchChannels;
        }
    }
}