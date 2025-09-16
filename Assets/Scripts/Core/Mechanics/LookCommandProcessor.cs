using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core
{
    public class LookCommandProcessor: IMessageProcessor
    {
        private readonly StringBuilder _builder = new();
        private readonly RoomConfig _roomConfig;
        private readonly RoomProgress _roomProgress;
        private readonly Messages.MessageHistory _history;

        public LookCommandProcessor(RoomConfig roomConfig, RoomProgress roomProgress, Messages.MessageHistory history)
        {
            _roomConfig = roomConfig;
            _roomProgress = roomProgress;
            _history = history;
        }
        
        public bool TryProcess(IReadOnlyCollection<string> message)
        {
            if (message.First() != "look") return false;

            _builder.Clear();
            _builder.Append(_roomConfig.InitialMessage);
            var allItems = _roomProgress.SurroundingItems.Concat(_roomConfig.LockedItems).Concat(_roomConfig.SurroundingInfos);
            var count = allItems.Count();
            if (allItems.Any())
            {
                _builder.AppendLine();
                _builder.Append("Stuff around: ");
                
                int i = 0;
                foreach (var item in allItems)
                {
                    _builder.Append(item.Name);
                    if (i++ < count - 1)
                    {
                        _builder.Append(", ");
                    }
                }
            }
            
            _history.AddMessage(new Message(_builder.ToString() , MessageType.Environment));

            return true;
        }
    }
}