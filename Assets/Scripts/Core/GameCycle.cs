using System;

namespace Core
{
    public class GameCycle
    {
        public event Action Ended;
        public event Action Started;
        public event Action PlayerClickedNext; 
        
        public bool IsCompleted { get; private set; }
        
        public void Start()
        {
            Started?.Invoke();
        }

        public void End()
        {
            Ended?.Invoke();
        }

        public void Next()
        {
            IsCompleted = true;
        }
    }
}