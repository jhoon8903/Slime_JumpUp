using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class ScenesManager
    {
        public static string NextScene { get; set; }

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

        public void LoadToScene(string nextScene)
        {
            NextScene = nextScene;
            SceneManager.LoadScene("LoadingScene");
        }
    }
}