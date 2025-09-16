using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Configs/LockedItem")]
    public class LockedItemConfig : ItemConfig
    {
        [field: SerializeField] public ItemConfig[] Keys { get; private set; }
    }
}