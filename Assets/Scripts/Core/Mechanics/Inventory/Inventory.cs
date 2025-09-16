using System.Collections.Generic;

namespace Core
{
    public class Inventory
    {
        public IEnumerable<ItemConfig> Items => _items;
        private readonly HashSet<ItemConfig> _items = new();
        
        public void AddItem(ItemConfig item)
        {
            _items.Add(item);
        }

        public void RemoveItems(ItemConfig[] recipeIngredients)
        {
            foreach (var ingredient in recipeIngredients)
            {
                _items.Remove(ingredient);
            }
        }
    }
}