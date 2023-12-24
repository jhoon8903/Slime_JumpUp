using System;
using System.Collections.Generic;
using Manager;
using Resource.Interface;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Resource
{
    public class AtlasLoader : IResourceLoader
    {
        public void LoadResource<T>(string key, Action<T> callback) where T : Object
        {
            AsyncOperationHandle<IList<Sprite>> handle = Addressables.LoadAssetAsync<IList<Sprite>>(key);
            AtlasCallback(key, handle, atlas => callback?.Invoke((T)atlas));
        }

        private void AtlasCallback<T>(string key, AsyncOperationHandle<IList<T>> handle, Action<IList<T>> callback) where T : Object
        {
            ResourceManager resource = ServiceLocator.GetService<ResourceManager>();
            handle.Completed += operationHandle =>
            {
                IList<T> resultList = operationHandle.Result;
                foreach (var result in resultList)
                {
                    string keyIndex = result.ToString().Split("_")[1].Split(" ")[0];
                    string resourceKey = $"{key}[{keyIndex}]";
                    resource.AddResource(resourceKey, result);
                }
                callback?.Invoke(resultList);
            };
        }
    }
}