using System;
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
        public event Action CharacterRespawn;
        private ConfigurableJoint[] _configurableJoints;
        private CharacterJoint[] _characterJoints;
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

        private void SetupCharacterJoint(CharacterJoint[] characterJoints)
        {
            foreach (var joint in characterJoints)
            {
                SoftJointLimit softJointLimit = new SoftJointLimit();
                softJointLimit.limit = 20; // Adjust for desired flexibility
                joint.swing1Limit = softJointLimit;
                joint.swing2Limit = softJointLimit;
                joint.lowTwistLimit = softJointLimit;
                joint.highTwistLimit = softJointLimit;
                JointDrive jointDrive = new JointDrive();
                jointDrive.positionSpring = 0; // Low spring for fluidity
                jointDrive.positionDamper = 0; // Low damper for fluidity
                jointDrive.maximumForce = Mathf.Infinity;
                joint.massScale = 1;
                joint.connectedMassScale = 1;
            }
        }

        private void SetupJoint(ConfigurableJoint[] joints)
        {
            foreach (ConfigurableJoint joint in joints)
            {
                // 관절 설정
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;
                joint.angularXMotion = ConfigurableJointMotion.Limited;
                joint.angularYMotion = ConfigurableJointMotion.Limited;
                joint.angularZMotion = ConfigurableJointMotion.Limited;
            
                // 관절의 유연성과 각도 제한 설정
                joint.lowAngularXLimit = new SoftJointLimit { limit = -90f }; // 예시로 -90도 설정
                joint.highAngularXLimit = new SoftJointLimit { limit = 90f }; // 예시로 90도 설정
                joint.angularYLimit = new SoftJointLimit { limit = 90f }; // 예시로 90도 설정
                joint.angularZLimit = new SoftJointLimit { limit = 90f }; // 예시로 90도 설정
            
                // 관절 드라이브 조정
                JointDrive drive = new JointDrive
                {
                    positionSpring = 500000f, // 더 부드러운 움직임을 위해 감소
                    positionDamper = 0f, 
                    maximumForce = Mathf.Infinity
                };
                joint.angularXDrive = drive;
                joint.angularYZDrive = drive;
                joint.slerpDrive = drive;
                joint.xDrive = drive;
                joint.yDrive = drive;
                joint.zDrive = drive;
            }
        }

        private void FixedUpdate()
        {
            if (gameObject.transform.position.y < -4f) OnCharacterRespawn();
        }

        private void OnCharacterRespawn()
        {
            CharacterRespawn?.Invoke();
        }
    }
}