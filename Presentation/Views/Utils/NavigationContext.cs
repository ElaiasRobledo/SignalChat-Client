using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Utils
{
    public class NavigationContext
    {
        public string? SelectedFriend { get; set; }
        public string FriendId { get; set; }
        public Screens CurrentScreen { get; set; }
    }
}
