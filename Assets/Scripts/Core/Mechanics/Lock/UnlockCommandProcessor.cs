using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class UnlockCommandProcessor: IMessageProcessor
    {
        private readonly RoomConfig _roomConfig;
        private readonly Inventory _inventory;
        private readonly Messages.MessageHistory _messageHistory;
        private readonly GameCycle _gameCycle;

        public UnlockCommandProcessor(RoomConfig roomConfig, Inventory inventory,
            Messages.MessageHistory messageHistory, GameCycle gameCycle)
        {
            _roomConfig = roomConfig;
            _inventory = inventory;
            _messageHistory = messageHistory;
            _gameCycle = gameCycle;
        }

        public bool TryProcess(IReadOnlyCollection<string> message)
        {
            if (message.Count < 2 || message.First() != "unlock") return false;

            var itemName = string.Join(' ', message.Skip(1));
            var item = _roomConfig.LockedItems.FirstOrDefault(i =>
                i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if(item == null)
            {
                _messageHistory.AddMessage(new Message($"You can't unlock this", MessageType.Error));
            }
            else if(item.Keys.All(k => _inventory.Items.Contains(k)))
            {
                _messageHistory.AddMessage(new Message(item.Description, MessageType.Environment));
                if(item.IsFinal) 
                    _gameCycle.End();
            }
            else
            {
                _messageHistory.AddMessage(new Message($"Can't unlock: {itemName}", MessageType.Error));
            }

            return true;
        }
    }
}