using Manager;
using UI.GameScene;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Scene
{
    public class BaseScene : MonoBehaviour
    {
        public static Label CurrentScene { get; set; }
        protected ResourceManager Resource;
        protected UIManager UI;
        protected ScenesManager Scenes;
        private ObstacleManager _obstacleManager;
        protected BackGround BackGround;
        private const string Label = "Preload";
        protected float SpawnDelay;
        private float _lastSpawn;

        private void Awake()
        {
            Resource = ServiceLocator.GetService<ResourceManager>();
            UI = ServiceLocator.GetService<UIManager>();
            Scenes = ServiceLocator.GetService<ScenesManager>();
            BackGround = ServiceLocator.GetService<BackGround>();
            _obstacleManager = ServiceLocator.GetService<ObstacleManager>();
        }

        private void Start()
        {
            if (Resource.Preload) Initialize();
            else LoadAssemble();
        }

        protected virtual void Initialize()
        {
            InstantiateEventSystem();
            InstantiateResolutionController();
        }

        private void InstantiateEventSystem()
        {
            Object eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem == null) Resource.InstantiateObject("EventSystem").name = "@EventSystem";
        }

        private void InstantiateResolutionController()
        {
            Object resolutionController = FindObjectOfType<ResolutionController>();
            if (resolutionController == null) Resource.InstantiateObject("ResolutionController").name = "@ResolutionController";
        }

        private void LoadAssemble()
        {
            Resource.AllLoadResource<Object>($"{Label}", (key, count, totalCount) =>
            {
                Debug.Log($"[{Label}] Load asset {key} ({count}/{totalCount})");
                if (count < totalCount) return;
                Resource.Preload = true;
                Initialize();
            });
        }

        protected virtual void FixedUpdate()
        {
            _lastSpawn += Time.deltaTime;
            if (!(SpawnDelay < _lastSpawn)) return;
            InstantiateObstacle();
            _lastSpawn = 0.0f;
        }

        private void InstantiateObstacle()
        {
            _obstacleManager.SpawnObstacle();
        }
    }
}