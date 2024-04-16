using System;
using DG.Tweening;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace Modules.UI.Windows.UnitUpgrade
{

    public class UnitUpgradeWindow : AbstractWindow
    {
        private const float AnimationDuration = .3f;

        private RectTransform _window;

        protected override void Awake()
        {
            base.Awake();
            _window = transform.GetChild(0).GetComponent<RectTransform>();
        }

        protected override async void Start()
        {
            ShowWithEffect();
            await AnimateOpacity(0, 1);
            base.Start();
        }

        private void ShowWithEffect()
        {
            var rectTransform = _window;
            var windowHeight = rectTransform.rect.height / 4;
            rectTransform.anchoredPosition = new Vector2(0, -windowHeight);
            rectTransform.DOAnchorPosY(0, AnimationDuration).SetEase(Ease.OutBack);
        }

        private TweenerCore<Vector2, Vector2, VectorOptions> HideWithEffect()
        {
            var rectTransform = _window;
            var windowHeight = rectTransform.rect.height / 4;
            rectTransform.anchoredPosition = new Vector2(0, 0);
            return rectTransform.DOAnchorPosY(-windowHeight, AnimationDuration).SetEase(Ease.InBack);
        }

        private async UniTask AnimateOpacity(float startValue, float endValue)
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = startValue;
            await canvasGroup.DOFade(endValue, AnimationDuration).ToUniTask();
        }

        protected override async void CloseWindow()
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            var rectTransform = _window;
            var windowHeight = rectTransform.rect.height / 4;

            canvasGroup.alpha = 1;
            canvasGroup.DOFade(0, AnimationDuration);
            await rectTransform.DOAnchorPosY(-windowHeight, AnimationDuration)
                .SetEase(Ease.Linear)
                .ToUniTask();

            base.CloseWindow();
        }
    }
}