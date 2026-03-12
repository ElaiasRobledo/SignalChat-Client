using Application.DTOs;
using Application.Interfaces.Channels;
using Presentation.Views.Utils;
using Spectre.Console;

namespace Presentation.Views.Handlers.Channels
{
 public static class CreateChannelHandler
{
    public static async Task<Screens> Show(
        string token,
        IChannels channelsService,
        NavigationContext navigationContext)
    {
        Helpers helpers = new Helpers();
        helpers.DrawHeader();

        AnsiConsole.MarkupLine("[bold green]Create new channel[/]");
        AnsiConsole.WriteLine();

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Channel [green]name[/]:")
                .Validate(n =>
                {
                    if (string.IsNullOrWhiteSpace(n))
                        return ValidationResult.Error("[red]Name cannot be empty[/]");
                    return ValidationResult.Success();
                }));

        var description = AnsiConsole.Prompt(
            new TextPrompt<string>("Channel [green]description[/]:")
                .AllowEmpty());

        var tagsInput = AnsiConsole.Prompt(
            new TextPrompt<string>("Tags (comma separated):")
                .AllowEmpty());

        var tags = string.IsNullOrWhiteSpace(tagsInput)
            ? new List<string>()
            : tagsInput.Split(",", StringSplitOptions.RemoveEmptyEntries)
                       .Select(t => t.Trim())
                       .ToList();

        var isPublic = AnsiConsole.Confirm("Is the channel [green]public[/]?");
        var isPublicName = AnsiConsole.Confirm("Is the [green]name visible publicly[/]?");
        var isVisible = AnsiConsole.Confirm("Should the channel be [green]visible[/]?");

        var request = new CreateChannelDto(
            name,
            description,
            tags,
            isPublic,
            isPublicName,
            isVisible
        );

        var result = await channelsService.CreateAsync(token, request);

        AnsiConsole.WriteLine();

        if (result.IsSuccessStatusCode)
        {
            AnsiConsole.MarkupLine("[green]Channel created successfully.[/]");
        }
        else
        {
            var error = await result.Content.ReadAsStringAsync();
            AnsiConsole.MarkupLine($"[red]{Markup.Escape(error)}[/]");
        }

        AnsiConsole.MarkupLine("\n[grey]Press any key to continue...[/]");
        Console.ReadKey(true);

        return Screens.ChannelsMenu;
    }
}




}