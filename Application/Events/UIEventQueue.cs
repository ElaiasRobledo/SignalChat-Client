using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Application.Events
{
    public sealed class UIEventQueue
    {
        private readonly ConcurrentQueue<UiEvent> _queue = new();

        public void Enqueue(UiEvent ev) => _queue.Enqueue(ev);

        public int CountIncomingMessages() => _queue.Count;   

        public bool TryDequeue(out UiEvent ev)
            => _queue.TryDequeue(out ev);
    }
}
