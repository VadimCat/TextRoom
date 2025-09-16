using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Messages
{
    public class MessageHistoryView : MonoBehaviour
    {
        [SerializeField] private MessageView _messageViewPrefab;
        [SerializeField] private RectTransform _root;

        private readonly Dictionary<int, MessageView> _messageViews = new();
    
        private MessageHistory _history;

        public void Construct(MessageHistory history)
        {
            _history = history;
            _history.HistoryUpdated += UpdateView;
        
            for (var i = 0; i < history.Capacity; i++)
            {
                _messageViews[i] = Instantiate(_messageViewPrefab, _root);
            }
        }

        private void UpdateView(IReadOnlyCollection<Message> messages)
        {
            int firstMessageIndex = _history.Capacity - messages.Count;
            for (int i = 0; i < firstMessageIndex; i++)
            {
                _messageViews[i].Reset();
            }

            for (int i = 0; i < messages.Count; i++)
            {
                _messageViews[i + firstMessageIndex].SetMessage(messages.Skip(i).First());
            }
        
            LayoutRebuilder.ForceRebuildLayoutImmediate(_root);
        }

        private void OnDestroy()
        {
            _history.HistoryUpdated -= UpdateView;
        }
    }
}