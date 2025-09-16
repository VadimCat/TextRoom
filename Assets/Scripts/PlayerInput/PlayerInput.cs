using Core;
using Messages;

namespace PlayerInput
{
    public class PlayerInput
    {
        private readonly MessageProcessingPipeline _messageProcessingPipeline;

        public PlayerInput(MessageProcessingPipeline messageProcessingPipeline)
        {
            _messageProcessingPipeline = messageProcessingPipeline;
        }
    
        public void ProcessInput(string input)
        {
            input = input?.Trim()?.ToLowerInvariant();
            if (input is { Length: > 0 })
            {
                _messageProcessingPipeline.PushMessage(new Message(input, MessageType.Player));
            }
        }
    }
}