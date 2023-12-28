using System;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Events
{
    public static class UIEvents
    {
        public static void SetEvent(this GameObject gameObject, UIEventType uiEventType, Action<PointerEventData> action)
        {
            UIEventsHandler handler = Utility.GetAddComponent<UIEventsHandler>(gameObject);
            handler.BindEvent(uiEventType, action);
        }
    }
}