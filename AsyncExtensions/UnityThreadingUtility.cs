using System;
using System.Threading;
using UnityEngine;

namespace Disorder.Unity.Core.Extensions.Await
{
    public static class UnityThreadingUtility
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
            if (SynchronizationContext.Current == UnityThreadingUtility.UnitySynchronizationContext)
            {
                action.Invoke();
            }
            else
            {
                UnityThreadingUtility.UnitySynchronizationContext.Post(_ => action.Invoke(), null);
            }
        }
    }
}