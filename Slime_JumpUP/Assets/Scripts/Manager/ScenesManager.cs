using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class ScenesManager
    {
        public string NextScene { get; set; }

        private GameObject _baseObjects;
        public GameObject BaseObjects
        {
            get
            {
                if (_baseObjects == null)
                {
                    _baseObjects = GameObject.Find("@BASE_Object") ?? new GameObject("@BASE_Object");
                }
                return _baseObjects;
            }
        }
    }
}