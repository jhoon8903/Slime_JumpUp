using System;
using Object = UnityEngine.Object;

namespace Resource.Interface
{
    public interface IResourceLoader
    {
        void LoadResource<T>(string key, Action<T> callback) where T : Object;
    }
}