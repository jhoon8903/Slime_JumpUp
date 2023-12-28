using UI.GameScene;
using UnityEngine;

namespace Scene
{
    public class GameScene : BaseScene
    {
        private Transform _cameraTransform;
        private Game_UI _gameUI;
        private const string Background = "GameBackground";
        protected override void Initialize()
        {
            base.Initialize();
            CurrentScene = Label.IntroScene;
            InstantiateCamera();
            InstantiateGameUI();
            InstantiateFloor();
            InstantiatePlayer();
            BackGround.InstantiateBackGround(Background, _cameraTransform);
        }

        private void InstantiateCamera()
        {
            Transform parent = Scenes.BaseObjects.transform;
            GameObject virtualCamera = Resource.InstantiateObject("Virtual_Camera", parent);
            virtualCamera.name = "@Camara & BackGround";
            _cameraTransform = virtualCamera.transform;
        }

        private void InstantiateGameUI()
        {
            _gameUI = UI.InstantiateSceneUI<Game_UI>();
            Canvas uiCanvas = _gameUI.GetComponent<Canvas>();
            uiCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            uiCanvas.worldCamera = Camera.main;
            uiCanvas.sortingOrder = 3;
        }
        
        private void InstantiateFloor()
        {
            Transform parent = Scenes.BaseObjects.transform;
            GameObject floorObject = Resource.InstantiateObject("Floor", parent);
            floorObject.name = "@Floor";
            floorObject.transform.position = Vector3.zero;
        }

        private void InstantiatePlayer()
        {
            Transform parent = Scenes.BaseObjects.transform;
            Vector3 playerSpawnPosition = new Vector3(0, 4f, 0);
            GameObject player = Resource.InstantiateObject("Player", parent);
            player.name = "@Player";
            player.transform.position = playerSpawnPosition;
        }

        protected override void FixedUpdate()
        {
            SpawnDelay = 1.5f;
            base.FixedUpdate();
        }
    }
}