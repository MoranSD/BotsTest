using Interfaces;

namespace Tools
{
    public class StateMachine
    {
        public IState CurrentState { get; private set; }
        public StateMachine(IState startState)
        {
            CurrentState = startState;
            CurrentState?.Enter();
        }
        public void ChangeState(IState state)
        {
            CurrentState = state;
            CurrentState?.Enter();
        }
    }
}
