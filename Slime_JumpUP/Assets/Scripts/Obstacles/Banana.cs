using UnityEngine;

namespace Obstacles
{
    public class Banana : Obstacle
    {
        protected override void OnEnable()
        {
            Delay = 10f;
            base.OnEnable();
        }
    }
}