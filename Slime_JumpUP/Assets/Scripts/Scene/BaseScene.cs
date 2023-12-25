using System;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Scene
{
    public class BaseScene : MonoBehaviour
    {
        public static Label CurrentScene { get; set; }
        protected ResourceManager resourceManager;
        protected UIManager uiManager;
        protected ScenesManager scenesManager;
        private void Awake()
        {
            resourceManager = ServiceLocator.GetService<ResourceManager>();
            uiManager = ServiceLocator.GetService<UIManager>();
            scenesManager = ServiceLocator.GetService<ScenesManager>();
        }

        private void Start()
        {
            if (resourceManager.Preload) Initialize();
            else
            {
                resourceManager.AllLoadResource<Object>("Preload", (key, count, totalCount) =>
                {
                    Debug.Log($"[Preload] Load asset {key} ({count}/{totalCount})");
                    if (count < totalCount) return;
                    resourceManager.Preload = true;
                    Initialize();
                });
            }
        }

        protected virtual void Initialize()
        {
            Object eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem == null) resourceManager.InstantiateObject("EventSystem").name = "@EventSystem";
        }
    }
}