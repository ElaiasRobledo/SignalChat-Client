using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class ChatMessage
    {
        public string Sender { get; }
        public string Content { get; }
        public DateTime Timestamp { get; }

        public ChatMessage(string sender, string content)
        {
            Sender = sender;
            Content = content;
            Timestamp = DateTime.UtcNow;
        }
    }
}
