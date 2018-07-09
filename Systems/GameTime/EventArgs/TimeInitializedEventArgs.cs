namespace UnityCore
{
    public class TimeInitializedEventArgs : System.EventArgs
    {
        public readonly int ElapsedInitialMinutes;

        public TimeInitializedEventArgs(int elapsedInitialMinutes)
        {
            ElapsedInitialMinutes = elapsedInitialMinutes;
        }
    }
}