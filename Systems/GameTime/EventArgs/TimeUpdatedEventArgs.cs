namespace UnityCore
{
    public class TimeUpdatedEventArgs : System.EventArgs
    {
        public readonly int CurrentElapsedMinutes;
        public readonly int PreviousElapsedMinutes;

        public TimeUpdatedEventArgs(int currentElapsedMinutes, int previousElapsedMinutes)
        {
            CurrentElapsedMinutes = currentElapsedMinutes;
            PreviousElapsedMinutes = previousElapsedMinutes;
        }
    }
}