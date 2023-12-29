using DG.Tweening;
using UnityEngine;

namespace Objects
{
    public class Cloud : MonoBehaviour
    {
        private const float Speed = 20f;
        private const float EndX = 10f;

        private void OnEnable()
        {
            MoveToCloud();
        }

        private void MoveToCloud()
        {
            transform
                .DOMoveX(EndX, Speed)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}