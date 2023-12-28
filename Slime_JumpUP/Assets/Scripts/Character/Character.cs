using System;
using Events;
using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Rigidbody characterBody;
        [SerializeField] private GameObject leftHand;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private GameObject leftFoot;
        [SerializeField] private GameObject rightFoot;
        public  Rigidbody CharacterBody { get; private set; }

        private ConfigurableJoint[] _configurableJoints;
        private CharacterJoint[] _characterJoints;
        private float _heightPosition;
        private CharacterEventHandler _eventHandler;
        private void Awake()
        {
            CharacterBody = characterBody;
            Utility.GetAddComponent<Sticky>(leftHand);
            Utility.GetAddComponent<Sticky>(rightHand);
            Utility.GetAddComponent<Sticky>(leftFoot);
            Utility.GetAddComponent<Sticky>(rightFoot);
            _characterJoints = GetComponentsInChildren<CharacterJoint>();
            SetupCharacterJoint(_characterJoints);
        }

        private void Start()
        {
            _eventHandler = ServiceLocator.GetService<CharacterEventHandler>();
        }

        private void SetupCharacterJoint(CharacterJoint[] characterJoints)
        {
            foreach (var joint in characterJoints)
            {

                joint.anchor = new Vector3(0, 0, 0);
                joint.axis = new Vector3(-1, 0, 0);
                joint.swingAxis = new Vector3(0, 0, 1);

                SoftJointLimit softJointLimit = new SoftJointLimit
                {
                    limit = 40, 
                    bounciness = 0,
                    contactDistance = 0
                };
                joint.swing1Limit = softJointLimit;
                joint.swing2Limit = softJointLimit;
                joint.lowTwistLimit = softJointLimit;
                joint.highTwistLimit = softJointLimit;

                SoftJointLimitSpring jointSpring = new SoftJointLimitSpring()
                {
                    spring = 100000f,
                    damper = 100000f,
                };
                joint.twistLimitSpring = jointSpring;
                joint.swingLimitSpring = jointSpring;

                joint.enableProjection = true;
                joint.projectionDistance = 0;
                joint.projectionAngle = 20;
                joint.breakForce = Mathf.Infinity;
                joint.breakTorque = Mathf.Infinity;

                joint.enableCollision = false;
                joint.enablePreprocessing = false;

                joint.massScale = 1;
                joint.connectedMassScale = 1;
            }
        }

        private void FixedUpdate()
        {
            if (gameObject.transform.position.y < -10f)
            {
                OnCharacterRespawn();
            }

            if (gameObject.transform.position.y > _heightPosition)
            {
                _heightPosition = gameObject.transform.position.y;
                OnUpdateHeight(_heightPosition);
            }
        }

        private void OnCharacterRespawn()
        {
            _eventHandler.OnCharacterRespawn();
        }

        private void OnUpdateHeight(float height)
        {
            _eventHandler.OnUpdateHeight(height);
        }
    }
}