using System;
using Constants.WindowsSettings;
using DG.Tweening;
using MessagePipe;
using Modules.Signals;
using UnityEngine;
using VContainer;

namespace Modules.UI.Components
{
    public class AnimateBarOnEnable : MonoBehaviour
    {
        [SerializeField] private GameObject bar;
        private ISubscriber<WindowSignal> _windowSignal;
        private IDisposable _subscribe;

        [Inject] private void Construct(ISubscriber<WindowSignal> windowSignal)
        {
            _windowSignal = windowSignal;
        }

        private void Start()
        {
            SetIntialZeroSize();
            _subscribe = _windowSignal.Subscribe(WindowSignalHandler);
        }

        private void OnDestroy()
        {
            _subscribe.Dispose();
        }

        private void WindowSignalHandler(WindowSignal signal)
        {
            if (signal.WindowID == WindowsNames.UnitUpgrade)
            {
                ShowAnimation();
            }
        }
        private void ShowAnimation()
        {
            bar.transform
                .DOScaleX(.3f, .7f)
                .SetEase(Ease.OutCubic);
        }
        private void SetIntialZeroSize()
        {
            var tr = bar.transform.localScale;
            tr.x = 0;
            bar.transform.localScale = tr;
        }
    }
}