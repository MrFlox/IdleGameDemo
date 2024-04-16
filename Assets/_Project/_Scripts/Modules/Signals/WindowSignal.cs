using Constants.WindowsSettings;

namespace Modules.Signals
{

    public record WindowSignal(WindowsNames WindowID, WindowStates WindowState)
    {
        public WindowsNames WindowID { get; } = WindowID;
        public WindowStates WindowState { get; } = WindowState;
    }
}