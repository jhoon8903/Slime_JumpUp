using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI
{
    public class UIBase : MonoBehaviour
    {
        private readonly Dictionary<Type, Object[]> _objects = new();
        private void BindUIComponents<T>(Type enumType) where T : Object
        {
            Binder.Binding<T>(gameObject, enumType, _objects);
        }

        protected void SetButton(Type type) => BindUIComponents<Button>(type);
        protected void SetImage(Type type) => BindUIComponents<Image>(type);
        protected void SetText(Type type) => BindUIComponents<TextMeshProUGUI>(type);
        protected void SetObject(Type type) => BindUIComponents<GameObject>(type);

        private T GetUIComponents<T>(int componentIndex) where T : Object
        {
            return Binder.Getter<T>(componentIndex, _objects);
        }

        protected Button GetButton(int componentIndex)
        {
            return GetUIComponents<Button>(componentIndex);
        }

        protected Image GetImage(int componentIndex)
        {
            return GetUIComponents<Image>(componentIndex);
        }

        protected TextMeshProUGUI GetText(int componentIndex)
        {
            return GetUIComponents<TextMeshProUGUI>(componentIndex);
        }

        protected GameObject GetObject(int componentsIndex)
        {
            return GetUIComponents<GameObject>(componentsIndex);
        }
    }
}