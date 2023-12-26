using System.Collections;
using UnityEngine;

namespace Character
{
    public class Controller : MonoBehaviour
    {
        private const float LaunchAddForce = 20f;
        private Camera _mainCamera;
        private Vector2 _dragStartPosition;
        private bool _isDragging = false;
        private Character _character;

        private void Start()
        {
            _mainCamera = Camera.main;
            _character = GetComponent<Character>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) StartDragging();
            if (!Input.GetMouseButtonUp(0) || !_isDragging) return;
            StopDragging();
            StartCoroutine(Launch());
        }

        private void StartDragging()
        {
            if (Camera.main == null) return;
            Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _dragStartPosition = new Vector2(mousePos.x, mousePos.y);
            _isDragging = true;
        }

        private void StopDragging()
        {
            _isDragging = false;
        }

        private IEnumerator Launch()
        {
            Vector2 dragEndPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 launchDirection = _dragStartPosition - dragEndPosition;
            float launchForce = launchDirection.magnitude * LaunchAddForce;
            launchForce = Mathf.Clamp(launchForce, 0, LaunchAddForce);
            _character.CharacterBody.AddForce(new Vector3(launchDirection.x, launchDirection.y,0) * launchForce, ForceMode.Impulse);
            yield return null;
        }
    }
}