using System;
using Manager;

namespace UI
{
    public class Popup : UIBase
    {
        protected virtual void InitializePopup()
        {
            ServiceLocator.GetService<UIManager>().OrderLayerToCanvas(gameObject);
        }
    }
}