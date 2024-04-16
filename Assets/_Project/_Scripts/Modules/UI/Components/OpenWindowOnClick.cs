using Constants.WindowsSettings;
using MessagePipe;
using Modules.Signals;
using UnityEngine;
using VContainer;

namespace Modules.UI.Components
{
    public class OpenWindowOnClick : MonoBehaviour
    {

        [SerializeField] private WindowsNames _openWindowOnClick;
        private IPublisher<OpenWindowSignal> _windowSignal;

        [Inject] private void Construct(IPublisher<OpenWindowSignal> windowSignal)
        {
            _windowSignal = windowSignal;
        }

        private void OnMouseDown()
        {
            _windowSignal.Publish(new OpenWindowSignal(_openWindowOnClick));
        }
    }
}