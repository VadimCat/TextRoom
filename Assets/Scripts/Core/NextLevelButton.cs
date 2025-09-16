using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class NextLevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private GameCycle _gameCycle;

        public void Construct(GameCycle gameCycle)
        {
            _gameCycle = gameCycle;
            _button.onClick.AddListener(_gameCycle.Next);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(_gameCycle.Next);
        }
    }
}