using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.IntroScene
{
    public class Intro_UI : UIBase
    {
        private TextMeshProUGUI _startText;
        private Image _fill;
        private int _maxCount;
        private const float Duration = 2f;

        protected override void Initialized()
        {
            base.Initialized();
            SetUpText();
            SetupImage();
        }

        private void SetUpText()
        {
            SetUI<TextMeshProUGUI>();
            _startText = GetUI<TextMeshProUGUI>("StartText");
            ChangedTextColor();
        }

        private void SetupImage()
        {
            SetUI<Image>();
            _fill = GetUI<Image>("Fill");
            _fill.fillAmount = 0f;
        }

        public void UpdateToProgress(int count, int totalCount, string key)
        {
            if (count == 0 && key == null) return;
            if (count == _maxCount) key = "Touch Your Screen";
            UpdateProgressBar(count, totalCount);
            UpdateText(key);
        }

        private void UpdateProgressBar(int count, int totalCount)
        {
            _maxCount = totalCount > _maxCount ? totalCount : _maxCount;
            _fill.fillAmount = (float)count / _maxCount;
            if (_fill.fillAmount >= 1f)  _maxCount = 0;
        }
        private void UpdateText(string key) => _startText.text = key;
        private void ChangedTextColor() =>  _startText.DOColor(Color.black, Duration).SetLoops(-1, LoopType.Yoyo);
    }
}