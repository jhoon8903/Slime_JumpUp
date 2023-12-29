using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UserInterface.IntroScene;
using Object = UnityEngine.Object;

namespace Scenes
{
    public class IntroScene  : BaseScene
    {
        private Intro_UI _introUI;
        private const string Background = "IntroBackground";
        private const string LabelString = "GameScene";
        protected override void Initialize()
        {
            base.Initialize();
            CurrentScene = Label.IntroScene;
            InstantiatePlayer();
            InstantiateIntroUI();
            LoadResource();
            BackGround.InstantiateBackGround(Background, Scenes.BaseObjects.transform);
        }
        private void InstantiatePlayer()
        {
            Transform parent = Scenes.BaseObjects.transform; 
            Vector3 playerSpawnPosition = new Vector3(0, 4f, 0);
            GameObject player = Resource.InstantiateObject("Player", parent);
            player.transform.position = playerSpawnPosition;
        }

        private void InstantiateIntroUI()
        { 
            _introUI = UI.InstantiateSceneUI<Intro_UI>(); 
        }

        private void LoadResource()
        { 
            Resource.AllLoadResource<Object>($"{LabelString}", (key, count, totalCount) =>
            {
                Debug.Log($"[{LabelString}] Load asset {key} ({count}/{totalCount})");
                _introUI.UpdateToProgress(count, totalCount, key);
                if (count < totalCount) return;
                Resource.GameSceneLoad = true;
            });
            AsyncOperation operation = LoadAsync(LabelString);
            StartCoroutine(GameSceneLoad(operation));
        }

        private AsyncOperation LoadAsync(string scene)
        { 
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
            operation.allowSceneActivation = false;
            return operation;
        }

        private IEnumerator GameSceneLoad(AsyncOperation operation)
        { 
            while (!Resource.GameSceneLoad && operation.progress < 0.9f) yield return null;
            while (!Input.GetMouseButtonDown(0)) yield return null;
            operation.allowSceneActivation = true;
        }

        protected override void FixedUpdate()
        {
            SpawnDelay = 1f;
            base.FixedUpdate();
        }
    }
}