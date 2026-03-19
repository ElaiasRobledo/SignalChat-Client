using Application.Services;
using Presentation.Views.Decorations;
using Presentation.Views.Utils;
using Presentation.Views.Utils.UIs;
using Spectre.Console;

public static class ChannelChatView
{
    public static async Task<Screens> ShowAsync(
        NavigationContext context,
        ChatService chatService,
        UiDispatcher dispatcher,
        UiState state)
    {
        state.CurrentScreen = Screens.ChannelsChat;
        state.ActiveChannelId = context.ChannelId;
        state.ActiveUsername = context.Username;

        AnsiConsole.Clear();
        Banner.Show();

        var tags = context.ChannelTags != null
            ? string.Join(", ", context.ChannelTags)
            : "-";

        var header = new Panel(
            $"[bold green]{context.ChannelName}[/]\n" +
            $"[grey]{context.ChannelDescription}[/]\n\n" +
            $"[yellow]Members:[/] {context.ChannelMembers}\n" +
            $"[blue]Tags:[/] {tags}"
        )
        .Border(BoxBorder.Rounded)
        .Header("[bold]Channel Info[/]");

        AnsiConsole.Write(header);

        AnsiConsole.MarkupLine("\n[grey]Type /exit to go back[/]\n");

        while (true)
        {
            dispatcher.ProccesGroupsEvents();

            if (Console.KeyAvailable || await WaitForInputAsync())
            {
                AnsiConsole.Write("You: ");

                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;

                if (string.Equals(input, "/exit", StringComparison.OrdinalIgnoreCase))
                    return Screens.MyChannels;

                await chatService.SendToGroupAsync(context.ChannelId, input);
            }
            else
            {
                await Task.Delay(50);
            }
        }
    }

    private static async Task<bool> WaitForInputAsync()
    {
        await Task.Delay(100);
        return Console.KeyAvailable;
    }
}