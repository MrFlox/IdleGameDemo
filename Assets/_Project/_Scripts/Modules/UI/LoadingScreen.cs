using System;
using MessagePipe;
using Modules.Signals;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Modules.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;

        private ISubscriber<LoadingSignal, float> _loadingSignal;
        private IDisposable _disposable;
        private float _progressValue;

        [Inject] private void Construct(ISubscriber<LoadingSignal, float> loadingSignal)
        {
            _loadingSignal = loadingSignal;
            _disposable = _loadingSignal.Subscribe(LoadingSignal.Progress, HandleProgressUpdate);
        }
        private void HandleProgressUpdate(float value) => _progressValue = value;

        private void Update() =>
            _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _progressValue, 3 * Time.deltaTime);

        private void OnDestroy() => _disposable.Dispose();
    }
}