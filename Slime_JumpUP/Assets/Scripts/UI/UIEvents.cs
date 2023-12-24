using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public static class UIEvents
    {
        public static void SetEvent(this GameObject gameObject, EventType eventType, Action<PointerEventData> action)
        {
            EventsHandler handler = Utility.GetAddComponent<EventsHandler>(gameObject);
            handler.BindEvent(eventType, action);
        }
    }
}