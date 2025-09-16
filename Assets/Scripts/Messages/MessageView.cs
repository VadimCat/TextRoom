using System.Linq;
using Core;
using TMPro;
using UnityEngine;

namespace Messages
{
    public class MessageView : MonoBehaviour
    {
        [SerializeField] private MessageStyleConfig _styleConfig;
        [SerializeField] private TMP_Text _text;

        public void SetMessage(Message message)
        {
            _text.text = message.text;
            var style = _styleConfig.Styles.FirstOrDefault(s => s.MessageType == message.type);

            _text.color = style.TextColor;
            _text.alignment = style.TextOptions;
        }

        public void Reset()
        {
            _text.text = string.Empty;
        }
    }
}