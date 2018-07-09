namespace UnityCore
{
    public class FiniteStateMachine
    {
        public IState CurrentState { get; private set; }

        IState previousState;

        public FiniteStateMachine(IState initialState = null)
        {
            if (initialState != null)
                EnterState(initialState);
        }

        public void EnterState(IState newState)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
                previousState = CurrentState;
            }

            CurrentState = null;
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void ExecuteCurrentState()
        {
            CurrentState?.Execute();
        }

        public void ExitCurrentState()
        {
            if (previousState == null)
            {
                CurrentState.Exit();
                CurrentState = null;
            }
            else
            {
                RestorePreviousState();
            }
        }

        public void RestorePreviousState()
        {
            if (previousState != null)
            {
                CurrentState.Exit();
                CurrentState = previousState;
                CurrentState.Enter();
            }
        }
    }
}