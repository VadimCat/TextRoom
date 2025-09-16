using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class ReadCommandProcessor : IMessageProcessor
    {
        private readonly RoomConfig _roomConfig;
        private readonly Messages.MessageHistory _messageHistory;

        public ReadCommandProcessor(RoomConfig roomConfig, Messages.MessageHistory messageHistory)
        {
            _roomConfig = roomConfig;
            _messageHistory = messageHistory;
        }

        public bool TryProcess(IReadOnlyCollection<string> message)
        {
            if (message.Count < 2 || message.First() != "read") return false;

            var itemName = string.Join(' ', message.Skip(1));
            var item = _roomConfig.SurroundingInfos.FirstOrDefault(i =>
                i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            _messageHistory.AddMessage(item != null
                ? new Message(item.Description, MessageType.Environment)
                : new Message($"No such item: {itemName}", MessageType.Error));

            return true;
        }
    }
}