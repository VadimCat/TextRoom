using System.Collections.Generic;

namespace Core
{
    public class RoomProgress
    {
        public RoomProgress(RoomConfig roomConfig)
        {
            _surroundingItems = new HashSet<ItemConfig>(roomConfig.SurroundingItems);
        }

        public IReadOnlyCollection<ItemConfig> SurroundingItems => _surroundingItems;
        private HashSet<ItemConfig> _surroundingItems { get; } = new();
        
        public void RemoveItem(ItemConfig item)
        {
            _surroundingItems.Remove(item);
        }
    }
}