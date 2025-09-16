using System;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Configs/Room")]
    public class RoomConfig : ScriptableObject
    {
        [field: SerializeField]
        public string InitialMessage { get; private set; } = string.Empty;
    
        [field: SerializeField]
        public string FinalMessage { get; private set; } = string.Empty;
        
        [field: SerializeField]
        public ItemConfig[] SurroundingItems { get; private set; } = Array.Empty<ItemConfig>();
        
        [field: SerializeField]
        public ItemConfig[] SurroundingInfos { get; private set; } = Array.Empty<ItemConfig>();
        
        [field: SerializeField]
        public LockedItemConfig[] LockedItems { get; private set; } = Array.Empty<LockedItemConfig>();
        
        [field: SerializeField]
        public RecipeConfig[] Recipes { get; private set; } = Array.Empty<RecipeConfig>();
    }
}