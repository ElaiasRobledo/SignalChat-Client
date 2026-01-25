using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Utils.UIs
{
    public sealed class UiState
    {
        public Screens CurrentScreen { get; set; }
        public string? ActiveChatUser { get; set; }

        public readonly Dictionary<string, List<string>> ChatHistory
            = new();
    }
}
