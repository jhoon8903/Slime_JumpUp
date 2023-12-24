using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using EventType = UI.EventType;

namespace Manager
{
    public class UIManager
    {
        private int _orderByLayer = 1;
        private Stack<Popup> _popupStack = new();
        private event Action Open;
        private event Action Close;

        private GameObject _baseUI;
        private GameObject BaseUI
        {
            get
            {
                if (_baseUI == null)
                {
                    _baseUI = GameObject.Find("@BASE_UI") ?? new GameObject("@BASE_UI");
                }
                return _baseUI;
            }
        }

        private string NameOfUI<T>(string uiName)
        {
            return string.IsNullOrEmpty(uiName) ? typeof(T).Name : uiName;
        }

        private T InstantiateUI<T>(string ui, Transform baseUITransform = null) where T : Component
        {
            GameObject obj = ServiceLocator.GetService<ResourceManager>().InstantiateObject(ui, baseUITransform);
            return Utility.GetAddComponent<T>(obj);
        }

        public T OpenPopup<T>(string uiName) where T : Popup
        {
            string ui = NameOfUI<T>(uiName);
            T popup = InstantiateUI<T>(ui, BaseUI.transform);
            popup.name = $"{uiName}";
            _popupStack.Push(popup);
            Open?.Invoke();
            return popup;
        }

        public void ClosePopup(Popup popup, List<EventType> eventTypes)
        {
            _popupStack.Pop();
            UnbindPopupEvents(popup, eventTypes);
            _orderByLayer--;
            Utility.DestroyUI(popup);
        }

        private void UnbindPopupEvents(Popup popup, List<EventType> eventTypes)
        {
            EventsHandler[] eventsHandler = popup.GetComponents<EventsHandler>();
            foreach (EventsHandler handler in eventsHandler)
            {
                foreach (EventType eventType in eventTypes)
                {
                    handler.UnbindEvent(eventType);
                }
            }
        }

        public void OrderLayerToCanvas(GameObject uiObject, bool sort = true)
        {
            Canvas canvas = Utility.GetAddComponent<Canvas>(uiObject);
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
            SortingOder(canvas, sort);
            CanvasScaler scales = Utility.GetAddComponent<CanvasScaler>(canvas.gameObject);
            scales.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scales.referenceResolution = new Vector2(1440, 2560);
            canvas.referencePixelsPerUnit = 100;
        }

        private void SortingOder(Canvas canvas, bool sort)
        {
            canvas.sortingOrder = sort ? _orderByLayer++ : 0;
        }
    }
}