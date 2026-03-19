using System.Text.Json;
using Application.DTOs;
using Application.Interfaces.Channels;
using Presentation.Views.Decorations;
using Presentation.Views.Utils;
using Spectre.Console;

public static class MyChannelsView
{
    public static async Task<Screens> ShowAsync(
        string token,
        IChannels channels,
        NavigationContext context)
    {
        AnsiConsole.Clear();
        Banner.Show();

        var response = await channels.GetMyChannelsAsync(token);
        var content = await response.Content.ReadAsStringAsync();
        var myChannels = JsonSerializer.Deserialize<List<MyChannelsDto>>(content) ?? new();

        if (myChannels.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold red]You are not in any channel[/]");
            AnsiConsole.MarkupLine("\n[grey]Press any key to go back...[/]");
            Console.ReadKey(true);

            return Screens.MainMenu;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[green]Channel[/]")
            .AddColumn("[yellow]Members[/]");
            //.AddColumn("[blue]Tags[/]");


        foreach (var c in myChannels)
        {
            // var tags = c.Tags != null
            //     ? string.Join(", ", c.Tags)
            //     : "-";
            table.AddRow(c.name, c.totalMembers.ToString());
        }

        AnsiConsole.Write(table);

        const string BackOption = "Back";

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .HighlightStyle(new Style(Color.Red))
                .PageSize(10)
                .Title("\n[bold]Select a channel[/]")
                .AddChoices(myChannels.Select(c => c.name))
                .AddChoices(BackOption)
        );


        if (selected == BackOption)
            return Screens.MainMenu;

        var channel = myChannels.First(c => c.name == selected);
        context.ChannelId = channel.id;
        context.ChannelName = channel.name;
        context.ChannelDescription = channel.description;
        //context.ChannelTags = channel.Tags;
        context.ChannelMembers = channel.totalMembers;

        return Screens.ChannelsChat;
    }
}