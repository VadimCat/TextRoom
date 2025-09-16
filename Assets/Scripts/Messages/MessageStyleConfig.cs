using System;
using Core;
using TMPro;
using UnityEngine;

namespace Messages
{
    [CreateAssetMenu(menuName = "Configs/MessageStyle")]
    public class MessageStyleConfig: ScriptableObject
    {
        [field: SerializeField] public MessageStyle[] Styles { get; private set; } = Array.Empty<MessageStyle>();
    }

    [Serializable]
    public class MessageStyle
    {
        [field: SerializeField] public MessageType MessageType { get; private set; } 
        [field: SerializeField] public Color TextColor { get; private set; }
        [field: SerializeField] public TextAlignmentOptions TextOptions { get; private set; }
    }
}