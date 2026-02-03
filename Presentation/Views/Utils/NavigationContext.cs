using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Views.Utils
{
    public class NavigationContext
    {
        public string? Username { get; set; }
        public string UserId { get; set; }
        public Screens CurrentScreen { get; set; }
    }
}
