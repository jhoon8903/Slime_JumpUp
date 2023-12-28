using System;
using DG.Tweening;
using Events;
using Obstacles;
using TMPro;
using UI.Popups;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.GameScene
{
    public class Game_UI : UIBase
    {
        private TextMeshProUGUI _score;
        private TextMeshProUGUI _retryCount;
        private TextMeshProUGUI _bananaScore;
        private GameObject _banana;
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
            SetUI<GameObject>();
            _banana = GetUI<GameObject>("Banana");
            GameObject banana = ResourceManager.InstantiateObject("Banana", _banana.transform);
            Banana bananaComponent = banana.GetComponent<Banana>();
            Destroy(bananaComponent);
            banana.transform.position = new Vector3(-210f, -90f, 0);
            banana.transform.localScale = new Vector3(500f, 500f, 500f);
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
            _score.text = $"{score} M";
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