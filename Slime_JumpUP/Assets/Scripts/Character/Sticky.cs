using UnityEngine;

namespace Character
{
    public class Sticky : MonoBehaviour
    {
        private int _stickyCount;
        private FixedJoint _joint;

        private void StickyObstacle(Collision obstacle)
        {
            ContactPoint contactPoint = obstacle.contacts[0];
            Vector3 collisionPoint = contactPoint.point;
            transform.position = new Vector3(collisionPoint.x, collisionPoint.y, 0);

            if (_joint == null)
            {
                _joint = gameObject.AddComponent<FixedJoint>();
                _joint.breakTorque = Mathf.Infinity;
                _joint.breakForce = Mathf.Infinity;
                _joint.connectedBody = obstacle.rigidbody;
            }
        }

        private void OnCollisionEnter(Collision obstacle)
        {    
            if (obstacle.gameObject.CompareTag("Obstacle") && _stickyCount < 1)
            {
                _stickyCount++;
                StickyObstacle(obstacle);
            }

            if (obstacle.gameObject.CompareTag("Banana"))
            {

            }
        }

        private void OnTriggerExit(Collider obstacle)
        {
            _stickyCount--;
            if (_stickyCount <= 0) _stickyCount = 0;
        }
    }
}