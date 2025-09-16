using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class CraftMessageProcessor: IMessageProcessor
    {
        private readonly RoomConfig _roomConfig;
        private readonly Inventory _inventory;
        private readonly Messages.MessageHistory _messageHistory;

        public CraftMessageProcessor(RoomConfig roomConfig, Inventory inventory,
            Messages.MessageHistory messageHistory)
        {
            _roomConfig = roomConfig;
            _inventory = inventory;
            _messageHistory = messageHistory;
        }

        public bool TryProcess(IReadOnlyCollection<string> message)
        {
            if (message.Count < 2 || message.First() != "gather") return false;

            var ingredients = message.Skip(1);
            var recipe = _roomConfig.Recipes.FirstOrDefault(r =>
                r.Ingredients.Length == ingredients.Count() &&
                r.Ingredients.All(i => ingredients.Contains(i.Name, StringComparer.OrdinalIgnoreCase)) &&
                ingredients.All(i => r.Ingredients.Any(ri => ri.Name.Equals(i, StringComparison.OrdinalIgnoreCase)))
            );

            if(recipe == null)
            {
                _messageHistory.AddMessage(new Message($"You can't gather this", MessageType.Error));
            }
            else
            {
                _inventory.RemoveItems(recipe.Ingredients);
                _inventory.AddItem(recipe.Result);
                _messageHistory.AddMessage(new Message($"You gathered: {recipe.Result.Name}", MessageType.Environment));
            }

            return true;
        }
    }
}