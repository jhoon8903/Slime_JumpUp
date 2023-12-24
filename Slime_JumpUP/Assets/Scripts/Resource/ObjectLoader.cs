using System;
using System.Collections.Generic;
using Manager;
using Resource.Interface;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Resource
{
    public class ObjectLoader : IResourceLoader
    {
        public void LoadResource<T>(string key, Action<T> callback) where T : Object
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            LoadCallback(key, handle, callback);
        }

        private void LoadCallback<T>(string key, AsyncOperationHandle<T> handle, Action<T> callback) where T : Object
        {
            ResourceManager resource = ServiceLocator.GetService<ResourceManager>();
            handle.Completed += operationHandle =>
            {
                resource.AddResource(key, operationHandle.Result);
                callback?.Invoke(operationHandle.Result);
            };
        }
    }
}