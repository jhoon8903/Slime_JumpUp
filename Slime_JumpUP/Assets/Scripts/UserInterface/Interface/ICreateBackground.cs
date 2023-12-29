using UnityEngine;

namespace UserInterface.Interface
{
    public interface ICreateBackground
    {
        void InstantiateBackGround(string objectName, Transform parent);

        void InstantiateSky(Transform parent);

        void InstantiateSun(Transform parent);

        void InstantiateCloud(Transform parent);
    }
}