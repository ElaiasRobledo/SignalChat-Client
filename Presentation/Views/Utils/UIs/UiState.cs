using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Utils.UIs
{
    public sealed class UiState
    {
        public Screens CurrentScreen { get; set; }
        public string? ActiveChatUser { get; set; } // este 
        public Guid? ActiverUserId { get; set; }  //add in the futuro private set for ActiveUserId  // o este estan de mas
        public string? ActiveChannelId { get; set; }
        public string? ActiveUsername { get; set; }

        public readonly Dictionary<string, List<string>> ChatHistory
            = new();

    }
}
