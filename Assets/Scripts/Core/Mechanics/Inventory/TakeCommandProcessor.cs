using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class TakeCommandProcessor : IMessageProcessor
    {
        private readonly Inventory _inventory;
        private readonly RoomProgress _roomProgress;
        private readonly Messages.MessageHistory _messageHistory;

        public TakeCommandProcessor(Inventory inventory, RoomProgress roomProgress,
            Messages.MessageHistory messageHistory)
        {
            _inventory = inventory;
            _roomProgress = roomProgress;
            _messageHistory = messageHistory;
        }

        public bool TryProcess(IReadOnlyCollection<string> message)
        {
            if (message.Count < 2 || message.First() != "take") return false;

            var itemName = string.Join(' ', message.Skip(1));
            var item = _roomProgress.SurroundingItems.FirstOrDefault(i =>
                i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            
            if (item != null)
            {
                _inventory.AddItem(item);
                _roomProgress.RemoveItem(item);
                _messageHistory.AddMessage(new Message($"You got {item.Name}\n{item.Description}",
                    MessageType.Environment));
            }
            else
            {
                _messageHistory.AddMessage(
                    new Message($"You cant take it", MessageType.Error));
            }

            return true;
        }
    }
}