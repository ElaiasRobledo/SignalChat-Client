using System.Text.Json;
using Application.DTOs;
using Application.Interfaces.Channels;
using Presentation.Views.Utils;
using Spectre.Console;

namespace Presentation.Views.Handlers.Channels;

public static class ChannelDetailsHandler
{
    private const string JoinOption = "Join Channel";
    private const string BackOption = "Back";

    public static async Task<Screens> ShowChannelDetails(
        string token,
        IChannels channelService,
        NavigationContext context)
    {
        Helpers helpers = new();
        helpers.DrawHeader();

        var channel = await GetChannelDetails(
            token,
            channelService,
            Guid.TryParse(context.ChannelId, out var channelId) ? channelId : Guid.Empty);

        if (channel is null)
        {
            AnsiConsole.MarkupLine("[red]Could not load channel[/]");
            Console.ReadKey();

            return Screens.SearchChannels;
        }

        RenderChannel(channel);

        var option = AskAction();

        return ResolveAction(option);
    }

    private static async Task<ChannelDetailsDto?> GetChannelDetails(
        string token,
        IChannels service,
        Guid channelId)
    {
        var response = await service.GetChannelById(token, channelId);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ChannelDetailsDto>(
            content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
    }

    private static void RenderChannel(ChannelDetailsDto channel)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[green1]Field[/]")
            .AddColumn("[green1]Value[/]");

        table.AddRow("Name", channel.Name);
        table.AddRow("Description", channel.Description);
        table.AddRow("Private", channel.IsPublic ? "Yes" : "No");
        table.AddRow("Tags", channel.Tags is null || !channel.Tags.Any()
        ? "No tags were found": string.Join(", ",channel.Tags));
        table.AddRow("Members", channel.TotalMembers.ToString());
        table.AddRow("Created At", channel.CreatedAt.ToString("yyyy-MM-dd"));

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    private static string AskAction()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Select an option[/]")
                .HighlightStyle(new Style(Color.Green3_1))
                .AddChoices(
                    JoinOption,
                    BackOption
                ));
    }

    private static Screens ResolveAction(string option)
    {
        return option switch
        {
            JoinOption => Screens.ChannelsChat,
            _ => Screens.SearchChannels
        };
    }
}
