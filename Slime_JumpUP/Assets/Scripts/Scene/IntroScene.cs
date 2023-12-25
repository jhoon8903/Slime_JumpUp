using Manager;
using UI.IntroScene;
using UnityEngine;

namespace Scene
{
    public class IntroScene  : BaseScene
    {
        protected override void Initialize()
        {
            base.Initialize();
            CurrentScene = Label.IntroScene;
            IntroUI();
            InitializeIntroObject();
        }

        private void InitializeIntroObject()
        {
            Transform parent = scenesManager.BaseObjects.transform; 
            resourceManager.InstantiateObject("IntroObject", parent);
        }

        private void IntroUI()
        {
           uiManager.InstantiateSceneUI<Intro_UI>();
        }
    }
}