using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        private const float Delay = 3f;
        private Coroutine _deactivationCoroutine;

        private void OnEnable()
        {
            _deactivationCoroutine = StartCoroutine(DeactivateAfterDelay());
        }

        private void OnDisable()
        {
            if (_deactivationCoroutine == null) return;
            StopCoroutine(_deactivationCoroutine);
            _deactivationCoroutine = null;
        }

        protected virtual IEnumerator DeactivateAfterDelay()
        {
            yield return new WaitForSeconds(Delay);
            ServiceLocator.GetService<PoolManager>().Push(gameObject);
        }
    }
}