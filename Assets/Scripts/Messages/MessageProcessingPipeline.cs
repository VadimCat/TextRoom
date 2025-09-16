using System;
using System.Collections.Generic;
using Core;

namespace Messages
{
    public class MessageProcessingPipeline
    {
        private readonly MessageHistory _history;
        private readonly List<IMessageProcessor> _commandProcessorPipeline;

        public MessageProcessingPipeline(MessageHistory history,
            IEnumerable<IMessageProcessor> processors)
        {
            _history = history;

            _commandProcessorPipeline = new List<IMessageProcessor>(processors);
        }

        public void PushMessage(Message message)
        {
            _history.AddMessage(message);

            var splitMessage = message.text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var processor in _commandProcessorPipeline)
            {
                if (processor.TryProcess(splitMessage))
                    return;
            }
            
            _history.AddMessage(new Message("I can't do it" , MessageType.Error));
        }
    }
}