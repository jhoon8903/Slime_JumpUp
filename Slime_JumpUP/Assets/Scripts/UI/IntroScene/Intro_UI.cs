using DG.Tweening;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.IntroScene
{
    public class Intro_UI : UIBase
    {
        private enum Texts { StartText }
        private TextMeshProUGUI _startText;
        private const float Duration = 2f;

        private void Start()
        {
            Initialized();
        }

        private void Initialized()
        {
            SetText(typeof(Texts));
            _startText = GetText((int)Texts.StartText);
            gameObject.SetEvent(EventType.Click, IntoTheGame);
            ChangedTextColor();
        }

        private void IntoTheGame(PointerEventData obj)
        {
            const string nextScene = "GameScene";
            ServiceLocator.GetService<ScenesManager>().LoadToScene(nextScene);
        }

        private void ChangedTextColor()
        {
            _startText.DOColor(Color.black, Duration).SetLoops(-1, LoopType.Yoyo);
        }
    }
}