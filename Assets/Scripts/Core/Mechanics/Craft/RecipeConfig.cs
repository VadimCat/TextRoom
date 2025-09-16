using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Configs/Recipe")]
    public class RecipeConfig: ScriptableObject
    {
        [field: SerializeField] public ItemConfig[] Ingredients { get; private set; }
        [field: SerializeField] public ItemConfig Result { get; private set; }
    }
}