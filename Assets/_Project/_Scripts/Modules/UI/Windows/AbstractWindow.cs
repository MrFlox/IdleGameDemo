using System.Collections.Generic;
using Constants.WindowsSettings;
using MessagePipe;
using Modules.Signals;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Modules.UI.Windows
{
    public class AbstractWindow : MonoBehaviour
    {
        [SerializeField] protected WindowsNames _windowName;
        [SerializeField] protected List<Button> _closeButtons;
        private IPublisher<WindowSignal> _windowSignal;

        [Inject] private void Construct(IPublisher<WindowSignal> windowSignal)
        {
            _windowSignal = windowSignal;
        }

        protected virtual void Awake()
        {
            InitCloseButtons();
        }

        protected virtual void Start()
        {
            _windowSignal.Publish(new WindowSignal(_windowName, WindowStates.Opened));
        }

        private void InitCloseButtons()
        {
            foreach (var button in _closeButtons)
                button.onClick.AddListener(CloseWindow);
        }

        protected virtual async void CloseWindow()
        {
            Destroy(gameObject);
        }

        private void RemoveListenersFromCloseButtons()
        {
            foreach (var button in _closeButtons)
                button.onClick.RemoveAllListeners();
        }

        private void OnDestroy() => RemoveListenersFromCloseButtons();
    }
}