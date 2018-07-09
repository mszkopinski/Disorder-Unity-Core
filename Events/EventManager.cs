using System;
using System.Linq;
using UnityEngine;

namespace UnityCore
{
    public class EventManager : MonoSingleton<EventManager>
    {
        [SerializeField] IEventService eventService;

        public void SendNotifications(GameEvent gameEvent, object args)
        {
            eventService?.SendNotifications(gameEvent.Name, args);

            if (gameEvent.Type == GameEventTypes.Single)
                RemoveAllListeners(gameEvent);
        }

        public void AddListener(GameEvent gameEvent, Action<object> onEventCallback)
        {
            eventService?.Subscribe(gameEvent.Name, onEventCallback);
        }

        public void RemoveListener(GameEvent gameEvent, Action<object> onEventCallback)
        {
            eventService?.Unsubscribe(gameEvent.Name, onEventCallback);
        }

        public void RemoveAllListeners(GameEvent gameEvent)
        {
            var keyToRemove = eventService?.Messages.Keys.FirstOrDefault(key => key == gameEvent.Name);

            if (keyToRemove != null)
            {
                eventService.Messages.Remove(keyToRemove);
            }
        }
    }
}