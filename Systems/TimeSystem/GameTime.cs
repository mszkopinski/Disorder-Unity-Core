using System;

namespace Disorder.Unity.Core
{
    [Serializable]
    public static class GameTime
    {
        public static int ElapsedDays => (int) TimeSpan.FromMinutes(ElapsedMinutes).TotalDays;

        public static int ElapsedHours => (int) TimeSpan.FromMinutes(ElapsedMinutes).TotalHours;

        public static int ElapsedMinutes
        {
            get => _elapsedMinutes;
            set
            {
                if (value == ElapsedMinutes)
                    return;

                var previousDays = ElapsedDays;

                _elapsedMinutes = value;
                
                OnTimeUpdated();
            }
        }

        public static int CurrentHour => (ElapsedHours % 24);
        public static int CurrentMinute => (ElapsedMinutes % 60);
        

        public static DayName CurrentDayName
        {
            get
            {
                int rest = ElapsedDays % 7;
                return rest == 1 ? DayName.Monday : rest == 2 ? DayName.Tuesday : rest == 3 ? DayName.Wednesday : rest == 4 ? DayName.Thursday : rest == 5 ? DayName.Friday : rest == 6 ? DayName.Saturday : DayName.Sunday;
            }
        }

        static int _elapsedMinutes;

        public static event EventHandler TimeUpdated;
        public static event EventHandler TimeInitialized;
        public static event EventHandler DayPassed;

        static GameTime()
        {        
            ElapsedMinutes = 0;
        }

        public static void InitializeTime(int days, int hours, int minutes)
        {
            ElapsedMinutes = ((days * 24 * 60) + hours * 60 + minutes);
        }

        public static void InitializeTime(int minutes)
        {
            ElapsedMinutes = minutes;
        }

        public static void Update(int days, int hours, int minutes)
        {
            ElapsedMinutes += (days * 24 * 60) + (hours * 60) + minutes;
        }

        /// <summary>
        /// Method always called when time updates. It raises TimeUpdated event. Also if days number has changed it raises DayPassed event.
        /// </summary>
        /// <param name="e"></param>
        static void OnTimeUpdated()
        {
            TimeUpdated?.Invoke(null, System.EventArgs.Empty);
        }

        /// <summary>
        /// Invokes TimeInitialized event whenever current time got initialized.
        /// </summary>
        static void OnTimeInitialized()
        {
            TimeInitialized?.Invoke(null, System.EventArgs.Empty);
        }

        /// <summary>
        /// Invokes DayPassed event whenever current time got initialized.
        /// </summary>
        static void OnDayPassed()
        {
            DayPassed?.Invoke(null, System.EventArgs.Empty);
        }

        public static string GetFormattedString => $"{ElapsedHours % 24:0}:{ElapsedMinutes % 60:00}"; 
        public static int DefaultMinutes => 60;
    }
}