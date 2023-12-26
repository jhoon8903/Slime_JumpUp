using Manager;
using UnityEngine;
using UnityEngine.Pool;

namespace Objects
{
    public class Pool
    {
        private readonly GameObject _prefab;
        private readonly IObjectPool<GameObject> _pool;
        private Transform _root;

        private Transform Root
        {
            get
            {
                if (_root != null) return _root;
                GameObject obj = new()
                {
                    name = $"[Pool_Root] {_prefab.name}"
                };            

                Transform baseObstacleTransform = ServiceLocator.GetService<ObstacleManager>().BaseObstacle.transform;
                obj.transform.SetParent(baseObstacleTransform, false);

                _root = obj.transform;
                return _root;
            }
        }

        public Pool(GameObject prefab)
        {
            _prefab = prefab;
            _pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
        }

        public GameObject Pop()
        {
            return _pool.Get();
        }

        public void Push(GameObject obj)
        {
            if (obj == null) return;
            _pool.Release(obj);
        }

        private GameObject OnCreate()
        {
            GameObject obj = Object.Instantiate(_prefab, Root, true);
            obj.name = _prefab.name;
            return obj;
        }

        private void OnGet(GameObject obj)
        {
            if (obj == null) return;
            obj.SetActive(true);
        }

        private void OnDestroy(GameObject obj)
        {
            Object.Destroy(obj);
        }

        private void OnRelease(GameObject obj)
        {
            if (obj == null) return;
            obj.SetActive(false);
        }
    }
}