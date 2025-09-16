using System;
using System.Collections.Generic;
using Core;

namespace Messages
{
    public class MessageHistory
    {
        private IReadOnlyCollection<Message> Messages => _messages;
        public event Action<IReadOnlyCollection<Message>> HistoryUpdated;

        public readonly int Capacity;
        private readonly Queue<Message> _messages = new();

        public MessageHistory(int capacity)
        {
            Capacity = capacity;
        }

        public void AddMessage(Message message)
        {
            _messages.Enqueue(message);
            if (_messages.Count > 10)
            {
                _messages.Dequeue();
            }

            HistoryUpdated?.Invoke(Messages);
        }
    }
}