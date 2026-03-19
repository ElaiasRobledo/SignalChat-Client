using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Utils
{
    public class NavigationContext
    {
        public string? Username { get; set; }
        public string? UserId { get; set; }
        public string? ChannelName {get; set;} 
        public string? ChannelId { get; set;}
        public string? ChannelDescription {get; set;}
        public List<string>? ChannelTags {get; set;}
        public int ChannelMembers {get; set;}
        public Screens CurrentScreen { get; set; }
    }
}
