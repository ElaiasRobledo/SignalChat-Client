using Application.Interfaces.Contacts;
using Presentation.Views.Decorations;
using Presentation.Views.Menus;
using Presentation.Views.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Handlers.Contacts
{
    public static class ApprovePendingRequestsHandler
    {
        public static async Task<Screens> ApproveOrRejectRequest(
            string token,
            IContacts contacts,
            NavigationContext context)
        {
            Banner.Show();

            var userId = context.UserId;
            var username = context.Username;

            if (userId == null || username == null)
                return Screens.FriendsMenu;

            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[green]Request from {username}[/]");

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices("Accept", "Reject", "Back"));

            if (option == "Accept")
                await contacts.ApproveContactsAsync(token, userId);

            if (option == "Reject")
                await contacts.RejectContactsAsync(token, userId);

            context.Username = null;
            context.UserId = null;

            LoginScreen.ShowStatus();



            return Screens.FriendsMenu;
        }
    }

}
