using Events;
using UnityEngine;

namespace Character
{
    public class CharacterSpawner : MonoBehaviour
    {
        private Character _character;
        private CharacterEventHandler _characterEventHandler;
        private void Start()
        {
            _character = GetComponent<Character>();
            _characterEventHandler = ServiceLocator.GetService<CharacterEventHandler>();
             _characterEventHandler.CharacterRespawn+= Respawn;
        }

        private void Respawn()
        {
            Rigidbody[] characterRigidBody = _character.GetComponentsInChildren<Rigidbody>();
            foreach (var rigidBody in characterRigidBody)
            {
                rigidBody.velocity = Vector3.zero;
            }
            _character.CharacterBody.transform.position = new Vector3(0, 1, 0);
        }
    }
}