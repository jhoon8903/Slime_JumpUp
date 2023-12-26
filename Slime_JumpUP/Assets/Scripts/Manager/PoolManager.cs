using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Manager
{
    public class PoolManager
    {
        private readonly Dictionary<string, Pool> _pools = new();

        public GameObject Pop(GameObject prefab)
        {
            if (!_pools.ContainsKey(prefab.name)) CreatePool(prefab);
            return _pools[prefab.name].Pop();
        }

        public bool Push(GameObject obj)
        {
            if (!_pools.ContainsKey(obj.name)) return false;
            _pools[obj.name].Push(obj);
            return true;
        }

        private void CreatePool(GameObject prefab)
        {
            Pool pool = new(prefab);
            _pools.Add(prefab.name, pool);
        }
    }
}