using System;
using Manager;
using UI.IntroScene;
using UnityEngine;

namespace Scene
{
    public class IntroScene  : BaseScene
    {
        private float _spawnDelay = 1f;
        private float _lastSpawn;
        protected override void Initialize()
        {
            base.Initialize();
            CurrentScene = Label.IntroScene;
            InstantiateIntroUI();
            InstantiateObject();

        }

        private void InstantiateObject()
        {
            Transform parent = scenesManager.BaseObjects.transform; 
            resourceManager.InstantiateObject("IntroObject", parent);
            Vector3 playerSpawnPosition = new Vector3(0, 4f, 0);
            GameObject player = resourceManager.InstantiateObject("Player", parent);
            player.transform.position = playerSpawnPosition;
        }

        private void InstantiateIntroUI()
        {
           uiManager.InstantiateSceneUI<Intro_UI>();
        }

        private void FixedUpdate()
        {
            _lastSpawn += Time.deltaTime;
            if (!(_spawnDelay < _lastSpawn)) return;
            InstantiateObstacle();
            _lastSpawn = 0.0f;
        }

        private void InstantiateObstacle()
        {
            ServiceLocator.GetService<ObstacleManager>().SpawnObstacle();
        }
    }
}