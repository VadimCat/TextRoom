using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageHistory = Messages.MessageHistory;

namespace Core
{
    public class CheckInventoryMessageProcessor: IMessageProcessor
    {
        public event Action<Message> MessageSent;
        
        private readonly Inventory _inventory;
        private readonly MessageHistory _messageHistory;

        private readonly StringBuilder _builder = new();

        public CheckInventoryMessageProcessor(Inventory inventory,
            MessageHistory messageHistory)
        {
            _inventory = inventory;
            _messageHistory = messageHistory;
        }
        
        public bool TryProcess(IReadOnlyCollection<string> message)
        {
            if (message.First() != "inventory") return false;
            
            var items = _inventory.Items.ToList();
            
            if(items.Count == 0)
            {
                _messageHistory.AddMessage(new Message("You have nothing", MessageType.Environment));
                return true;
            }

            _builder.Clear();
            _builder.Append("I have: ");

            
            for (var i = 0; i < items.Count; i++)
            {
                _builder.Append(items[i].Name);
                if (i < items.Count - 1)
                {
                    _builder.Append(", ");
                }
            } 
            _messageHistory.AddMessage(new Message(_builder.ToString(), MessageType.Environment));
            return true;
        }
    }
}