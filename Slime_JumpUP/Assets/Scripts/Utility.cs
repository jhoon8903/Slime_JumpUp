using UnityEngine;
using UserInterface;

public class Utility : MonoBehaviour
{        
    public static T GetAddComponent<T>(GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() ?? obj.AddComponent<T>();  
    }

    public static void DestroyUI(UIBase ui)
    {
        Destroy(ui.gameObject);
    }

    public static GameObject InstantiateObject(GameObject gameObject, Transform transform)
    {
        return Instantiate(gameObject, transform);
    }
}