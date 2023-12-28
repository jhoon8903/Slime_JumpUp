using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionController : MonoBehaviour
{
    private const float TargetAspectRatio = 9f / 19.5f;
    private const float MinWidth = 540f;
    private const float MinHeight = 1170;
    private Vector2 _lastScreenSize;
    private Coroutine _resizeCoroutine;
    private const float DebounceTime = 0.1f;

    private void Start()
    {
        _lastScreenSize = new Vector2(Screen.width, Screen.height);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        if (Mathf.Approximately(screenSize.x, _lastScreenSize.x) 
            && Mathf.Approximately(screenSize.y, _lastScreenSize.y)) return;

        if (_resizeCoroutine != null) StopCoroutine(_resizeCoroutine);

        _resizeCoroutine = StartCoroutine(DebounceResize(screenSize));
        _lastScreenSize = screenSize;
    }

    private IEnumerator DebounceResize(Vector2 newSize)
    {
        yield return new WaitForSeconds(DebounceTime);

        bool isWidthResized = !Mathf.Approximately(newSize.x, _lastScreenSize.x);

        if (isWidthResized)
        {
            int newHeight = Mathf.RoundToInt(newSize.x / TargetAspectRatio);
            
            if(newHeight < MinHeight)
            {
                newHeight = (int)MinHeight;
                newSize.x = Mathf.RoundToInt(newHeight * TargetAspectRatio);
            }
            Screen.SetResolution((int)newSize.x, newHeight, false);
        }
        else
        {
            int newWidth = Mathf.RoundToInt(newSize.y * TargetAspectRatio);
            if(newWidth < MinWidth)
            {
                newWidth = (int)MinWidth;
                newSize.y = Mathf.RoundToInt(newWidth / TargetAspectRatio);
            }
            
            Screen.SetResolution(newWidth, (int)newSize.y, false);
        }
    }
}
