using System;

namespace Services.Models
{
    public class GameState
    {
        public delegate void ChangeGameStateDelegate(State state);
        public event ChangeGameStateDelegate ChangeGameStateEvent;
        
        [Serializable]
        public enum State
        {
            Play,
            Pause,
            GameOver,
            MainMenu
        }

        private State _currentState;

        public void ChangeState(State newState)
        {
            _currentState = newState;
            ChangeGameStateEvent?.Invoke(_currentState);
        }
    }
}