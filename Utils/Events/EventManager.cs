using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Oxtail.Utils
{
    public static class EventManager<EventData> where EventData : struct
    {
        private static Dictionary<string, UnityEvent<EventData>> m_Events = new Dictionary<string, UnityEvent<EventData>>();

        public static void AddListener(UnityAction<EventData> listener)
        {
            string eventName = nameof(EventData);
            UnityEvent<EventData> evt = null;

            if (!m_Events.TryGetValue(eventName, out evt))
            {
                evt = new UnityEvent<EventData>();
                m_Events[eventName] = evt;
            }

            evt.AddListener(listener);
        }

        public static void RemoveListener(UnityAction<EventData> listener)
        {
            if (!m_Events.TryGetValue(nameof(EventData), out UnityEvent<EventData> evt))
                return;

            evt.RemoveListener(listener);
        }

        public static void RemoveAllListeners()
        {
            if (!m_Events.TryGetValue(nameof(EventData), out UnityEvent<EventData> evt))
                return;

            evt.RemoveAllListeners();
        }

        public static void TriggerEvent(EventData data)
        {
            if (!m_Events.TryGetValue(nameof(EventData), out UnityEvent<EventData> evt))
                return;

            evt?.Invoke(data);
        }
    }
}
