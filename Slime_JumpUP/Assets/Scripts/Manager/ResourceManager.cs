using System;
using System.Collections.Generic;
using Resource;
using Resource.Interface;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;

namespace Manager
{
    public class ResourceManager
    { 
        private Dictionary<string, Object> Resource { get; set; } = new();
        public bool Preload { get; set; }
        public bool IntroSceneLoad { get; set; }
        public bool GameSceneLoad { get; set; }
        private PoolManager _pool;
        private SpriteLoader _spriteLoader;
        private AtlasLoader _atlasLoader;
        private ObjectLoader _objectLoader;
        
        public void Initialize()
        {
            _pool = ServiceLocator.GetService<PoolManager>();
            _spriteLoader = ServiceLocator.GetService<SpriteLoader>();
            _atlasLoader = ServiceLocator.GetService<AtlasLoader>();
            _objectLoader = ServiceLocator.GetService<ObjectLoader>();
        }

        public void AddResource(string key, Object resource)
        {
            Resource.TryAdd(key, resource);
        }

        private IResourceLoader GetLoader(string key)
        {
            if (key.EndsWith(".sprite")) return _spriteLoader;
            if (key.EndsWith(".atlas")) return _atlasLoader;
            return _objectLoader;
        }

        public void AllLoadResource<T>(string label, Action<string, int, int> callback) where T : Object
        {
            AsyncOperationHandle<IList<IResourceLocation>> operation = Addressables.LoadResourceLocationsAsync(label, typeof(T));
            operation.Completed += operationHandle =>
            {
                int loadCount = 0;
                int totalCount = operationHandle.Result.Count;
                foreach (var result in operationHandle.Result)
                {
                    LoadResource<T>(result.PrimaryKey, obj =>
                    {
                        loadCount++;
                        callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                    });
                }
            };
        }

        private void LoadResource<T>(string key, Action<T> callback = null) where T : Object
        {
            IResourceLoader loader = GetLoader(key);
            loader?.LoadResource<T>(key, obj =>
            {
                Resource[key] = obj;
                callback?.Invoke(obj);
            });
        }

        public T Load<T>(string key) where T : Object
        {
            if (Resource.TryGetValue(key, out Object resource)) return resource as T;
            throw new KeyNotFoundException($"Not Found Key : {key}");
        }

        public GameObject InstantiateObject(string key, Transform parent = null, bool pooling = false)
        {
            GameObject resource = Load<GameObject>($"{key}.prefab");
            return pooling ? _pool.Pop(resource) : Utility.InstantiateObject(resource, parent);
        }
    }
}
