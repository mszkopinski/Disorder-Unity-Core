using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace UnityCore
{
    public class UnityCoroutineScheduler<T> : INotifyCompletion
    {
        public bool IsCompleted { get; private set; }

        Exception exception;
        Action continueWith;
        T result;

        public T GetResult()
        {
            ConditionHelper.Assert(IsCompleted);

            if (exception != null)
            {
                ExceptionDispatchInfo.Capture(exception).Throw();
            }

            return result;
        }

        public void Complete(T result, Exception e)
        {
            ConditionHelper.Assert(!IsCompleted);

            IsCompleted = true;
            exception = e;
            this.result = result;

            if (continueWith != null)
            {
                UnitySchedulerProvider.RunOnUnityScheduler(continueWith);
            }
        }

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            ConditionHelper.Assert(continueWith == null || !IsCompleted);

            continueWith = continuation;
        }
    }

    public class UnityCoroutineScheduler : INotifyCompletion
    {
        public bool IsCompleted { get; private set; }

        Exception exception;
        Action continueWith;

        public void GetResult()
        {
            ConditionHelper.Assert(IsCompleted);

            if (exception != null)
            {
                ExceptionDispatchInfo.Capture(exception).Throw();
            }
        }

        public void Complete(Exception e)
        {
            ConditionHelper.Assert(!IsCompleted);

            IsCompleted = true;
            exception = e;

            if (continueWith != null)
            {
                UnitySchedulerProvider.RunOnUnityScheduler(continueWith);
            }
        }

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            ConditionHelper.Assert(continueWith == null);
            ConditionHelper.Assert(!IsCompleted);

            continueWith = continuation;
        }
    }

    internal static class ConditionHelper
    {
        public static void Assert(bool condition)
        {
            if (condition)
                return;

            throw new Exception("Some condition is false in UnityCoroutineAwaiter...");
        }
    }
}