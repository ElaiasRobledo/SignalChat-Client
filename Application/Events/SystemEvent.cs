using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Events
{
    public sealed record SystemEvent
     (
         string Message
     ) : UiEvent;
}
