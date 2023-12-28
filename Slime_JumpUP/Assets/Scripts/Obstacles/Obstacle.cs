using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        protected float Delay;
        private Coroutine _deactivationCoroutine;

        protected virtual void OnEnable()
        {
            _deactivationCoroutine = StartCoroutine(DeactivateAfterDelay());
        }

        protected virtual void OnDisable()
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