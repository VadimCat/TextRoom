using TMPro;
using UnityEngine;

namespace PlayerInput
{
    public class PlayerInputView: MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        private PlayerInput _playerInput;

        public void Construct(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _inputField.onSubmit.AddListener(OnInputSubmitted);
            _inputField.ActivateInputField();
        }

        private void OnInputSubmitted(string input)
        {
            _playerInput.ProcessInput(input);
            _inputField.text = string.Empty;
            _inputField.ActivateInputField();
        }

        public void OnDestroy()
        {
            _inputField.onSubmit.RemoveListener(OnInputSubmitted);
        }
    }
}
