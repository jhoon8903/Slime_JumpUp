using DG.Tweening;
using Events;
using Obstacles;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UserInterface.Popups;

namespace UserInterface.GameScene
{
    public class Game_UI : UIBase
    {
        private TextMeshProUGUI _score;
        private TextMeshProUGUI _retryCount;
        private TextMeshProUGUI _bananaScore;
        private Transform _banana;
        private Button _pauseBtn;
        private int _retry;
        private const float BananaSpinSpeed = 5f;

        protected override void Initialized()
        {
            base.Initialized();
            SetupObject();
            SetupText();
            SetupButton();
            SetupEvents();
        }

        private void SetupText()
        {
            SetUI<TextMeshProUGUI>();
            _score = GetUI<TextMeshProUGUI>("Score");
            _retryCount = GetUI<TextMeshProUGUI>("RetryCount");
            _bananaScore = GetUI<TextMeshProUGUI>("BananaScore");
        }

        private void SetupButton()
        {
            SetUI<Button>();
            _pauseBtn = GetUI<Button>("PauseBtn");
            CharacterEventHandler.CharacterHeight += UpdateScore;
            CharacterEventHandler.CharacterRespawn += UpdateRetry;
        }

        private void SetupObject()
        {
            SetUI<Transform>();
            _banana = GetUI<Transform>("Banana");
            GameObject banana = ResourceManager.InstantiateObject("Banana", _banana);
            Banana bananaComponent = banana.GetComponent<Banana>();
            BoxCollider bananaCollider = banana.GetComponent<BoxCollider>();
            Destroy(bananaComponent);
            Destroy(bananaCollider);
            banana.transform.localPosition = new Vector3(-216f, -80f, 0);
            banana.transform.localScale = new Vector3(200f, 200f, 200f);
            RotateBanana(banana);
        }

        private void RotateBanana(GameObject banana)
        {
            banana.transform
                .DORotate(new Vector3(0, 360, 0), BananaSpinSpeed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }

        private void SetupEvents()
        {
            _pauseBtn.gameObject.SetEvent(UIEventType.Click,OpenPausePopup);
        }

        private void OpenPausePopup(PointerEventData eventData)
        {
            UIManager.OpenPopup<PausePopup>("PausePopup");
        }

        private void UpdateScore(float score)
        {
            _score.text = $"{score:00.0} M";
        }

        private void UpdateRetry()
        {
            _retry++;
            _retryCount.text = $"{_retry}";
        }

        private void GetBanana(int getBananaCount)
        {
        }
    }
}