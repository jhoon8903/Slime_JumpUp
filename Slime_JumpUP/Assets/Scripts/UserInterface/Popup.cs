using Manager;

namespace UserInterface
{
    public class Popup : UIBase
    {
        protected virtual void InitializePopup()
        {
            ServiceLocator.GetService<UIManager>().OrderLayerToCanvas(gameObject);
        }
    }
}