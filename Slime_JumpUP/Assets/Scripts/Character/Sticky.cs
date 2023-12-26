using System;
using System.Collections.Generic;
using Obstacles;
using UnityEngine;

namespace Character
{
    public class Sticky : MonoBehaviour
    {
        private static int _stickyCount;
        private SpringJoint _joint;
        public event Action ExitObstacle;

        private void StickyObstacle(GameObject obstacleGameObject)
        {
            if (_joint == null)
            {
                _joint = gameObject.AddComponent<SpringJoint>();
                _joint.spring = 500000;
                _joint.damper = 0;
                _joint.maxDistance = 0.1f;
            }
            _joint.connectedBody = obstacleGameObject.GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider obstacle)
        {    
            Obstacle obj = obstacle.GetComponent<Obstacle>();
            if (obstacle.CompareTag("Obstacle") && obj != null && _stickyCount <1)
            {
                _stickyCount++;
                StickyObstacle(obstacle.gameObject);
            }
        }

        private void OnTriggerExit(Collider obstacle)
        {
            _stickyCount--;
            if (_stickyCount <= 0) _stickyCount = 0;
        }
    }
}