using System.Collections.Generic;

namespace Core
{
    public interface IMessageProcessor
    {
        public bool TryProcess(IReadOnlyCollection<string> message);
    }
}