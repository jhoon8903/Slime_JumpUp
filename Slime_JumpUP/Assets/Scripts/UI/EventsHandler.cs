using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class EventsHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        private readonly Dictionary<EventType, Action<PointerEventData>> _eventHandlers = new();

        private void InvokeEventAction(EventType eventType, PointerEventData eventData)
        {
            if (_eventHandlers.TryGetValue(eventType, out var action)) action?.Invoke(eventData);
        }

        public void BindEvent(EventType eventType, Action<PointerEventData> action)
        {
            _eventHandlers[eventType] = action;
        }

        public void UnbindEvent(EventType eventType)
        {
            if (_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers.Remove(eventType);
            }
        }

        public void OnPointerClick(PointerEventData eventData) => InvokeEventAction(EventType.Click, eventData);
        public void OnPointerEnter(PointerEventData eventData) => InvokeEventAction(EventType.PointerEnter, eventData);
        public void OnPointerExit(PointerEventData eventData) => InvokeEventAction(EventType.PointerExit, eventData);

        private void OnDestroy()
        {
            _eventHandlers.Clear();
        }
    }
}