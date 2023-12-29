using Events;
using Manager;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI
{
    public class UIBase : MonoBehaviour
    {
        protected UIManager UIManager;
        protected CharacterEventHandler CharacterEventHandler;
        protected ResourceManager ResourceManager;

        private void Start()
        {
            Initialized();
        }

        protected virtual void Initialized()
        {
            UIManager = ServiceLocator.GetService<UIManager>();
            CharacterEventHandler = ServiceLocator.GetService<CharacterEventHandler>();
            ResourceManager = ServiceLocator.GetService<ResourceManager>();
        }
        protected void SetUI<T>() where T : Object =>  Binder.Binding<T>(gameObject);
        protected T GetUI<T>(string componentName) where T : Object => Binder.Getter<T>(componentName); 
    }
}