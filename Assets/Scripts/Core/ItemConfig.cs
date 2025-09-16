using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Configs/Item")]
    public class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; } = string.Empty;
        [field: SerializeField] public string Description { get; private set; } = string.Empty;
        [field: SerializeField] public bool IsFinal { get; private set; } = false;
    }
}