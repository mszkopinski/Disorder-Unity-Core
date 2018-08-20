using System;
using System.Collections.Generic;

namespace UnityCore
{
    public class PubSubService : IEventService
    {
        public IDictionary<string, List<Action<object>>> Messages { get; private set; }

        public PubSubService()
        {
            Messages = new Dictionary<string, List<Action<object>>>();
        }

        public void Subscribe(string name, Action<object> callback)
        {
            if (!Messages.ContainsKey(name))
            {
                var temp = new List<Action<object>> {callback};
                Messages.Add(name, temp);
            }
            else
            {
                bool exists = false;

                foreach (var value in Messages[name])
                {
                    if (value.Method.ToString() == callback.Method.ToString())
                        exists = true;
                }

                if (!exists)
                    Messages[name].Add(callback);
            }
        }

        public void Unsubscribe(string name, Action<object> callback)
        {
            if (!Messages.ContainsKey(name))
                return;

            Messages[name].Remove(callback);
        }

        public void SendNotifications(string name, object args)
        {
            if (!Messages.ContainsKey(name))
                return;

            foreach (var callback in Messages[name])
                callback.Invoke(args);
        }
    }
}