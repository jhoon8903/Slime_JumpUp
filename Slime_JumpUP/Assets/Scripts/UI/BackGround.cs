using Manager;
using Scene;
using UI.Interface;
using UnityEngine;

namespace UI.GameScene
{
    public class BackGround : ICreateBackground
    {
        private ResourceManager _resource;

        public void InstantiateBackGround(string objectName, Transform parent)
        {
            _resource = ServiceLocator.GetService<ResourceManager>();
            GameObject background = _resource.InstantiateObject(objectName, parent);
            Transform backgroundTransform = background.transform;
            InstantiateSky(backgroundTransform);
            InstantiateSun(backgroundTransform);
            InstantiateCloud(backgroundTransform);
        }

        public void InstantiateSky(Transform parent)
        {
            GameObject sky = _resource.InstantiateObject("Sky", parent);
            sky.transform.localPosition = new Vector3(0,0,2f);
            sky.transform.localScale = new Vector3(0.24f, 0.5f, 1f);
        }

        public void InstantiateSun(Transform parent)
        {
            GameObject sun = _resource.InstantiateObject("SunWhite", parent);
            sun.transform.localPosition = new Vector3(-1.15f, 3.4f, 0);
            sun.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }

        public void InstantiateCloud(Transform parent)
        {
            GameObject cloud = _resource.InstantiateObject("CloudWhite", parent);
            cloud.transform.localPosition = new Vector3(3.2f, -5.4f, 0f);
            cloud.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }
}