using Constants.WindowsSettings;

namespace Modules.Signals
{
    public record OpenWindowSignal(WindowsNames Window)
    {
        public WindowsNames Window { get; } = Window;
    }
}