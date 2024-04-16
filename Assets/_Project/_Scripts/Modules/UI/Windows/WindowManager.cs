using MessagePipe;
using Modules.Signals;
using VContainer.Unity;

namespace Modules.UI.Windows
{
    public class WindowManager : IStartable
    {
        private readonly ISubscriber<OpenWindowSignal> _windowsSignal;
        private readonly IWindowsFactory _windowsFactory;

        public WindowManager(
            ISubscriber<OpenWindowSignal> windowsSignal,
            IWindowsFactory windowsFactory)
        {
            _windowsSignal = windowsSignal;
            _windowsFactory = windowsFactory;
        }

        public void Start()
        {
            _windowsSignal.Subscribe(OnWindowHandler);
        }

        private void OnWindowHandler(OpenWindowSignal signal)
        {
            _windowsFactory.CreateWindow(signal.Window);
        }
    }
}