namespace Disorder.Unity.Core
{
    /// <summary>
    /// It is finite state machine basically but with cool name
    /// </summary>
    public class StateMachine
    {
        public IState CurrentState { get; private set; }

        IState previousState;

        /// <summary>
        /// Enters new state. If there was a running activity assign it to previous
        /// </summary>
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

        /// <summary>
        /// Executes current activity
        /// </summary>
        public void ExecuteCurrentState()
        {
            IState state = CurrentState;

            state?.Execute();
        }

        /// <summary>
        /// Restores previous activity if there was one
        /// </summary>
        public void RestorePreviousState()
        {
            if (previousState != null)
            {
                CurrentState.Exit();
                CurrentState = previousState;
                CurrentState.Enter();
            }
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
    }
}