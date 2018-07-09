using System;
using System.Threading;
using UnityEngine;

namespace UnityCore
{
    public static class UnitySchedulerProvider
    {
        public static SynchronizationContext UnitySynchronizationContext { get; private set; }
        public static int UnityThreadId { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Setup()
        {
            UnitySynchronizationContext = SynchronizationContext.Current;
            UnityThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public static void RunOnUnityScheduler(Action action)
        {
            if (SynchronizationContext.Current == UnitySynchronizationContext)
            {
                action?.Invoke();
            }
            else
            {
                UnitySynchronizationContext.Post(_ => action?.Invoke(), null);
            }
        }
    }
}