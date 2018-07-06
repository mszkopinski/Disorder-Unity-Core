using System;
using System.Collections.Generic;

namespace Disorder.Unity.Core
{
    public interface IMessagingService
    {
        IDictionary<string, List<Action<object>>> Messages { get; }

        void Subscribe(string name, Action<object> callback);
        void Unsubscribe(string name, Action<object> callback);
        void SendNotifications(string name, object args);
    }
}