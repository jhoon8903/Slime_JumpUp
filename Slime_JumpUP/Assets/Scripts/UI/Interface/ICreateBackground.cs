using UnityEngine;

namespace UI.Interface
{
    public interface ICreateBackground
    {
        void InstantiateBackGround(string objectName, Transform parent);

        void InstantiateSky(Transform parent);

        void InstantiateSun(Transform parent);

        void InstantiateCloud(Transform parent);
    }
}