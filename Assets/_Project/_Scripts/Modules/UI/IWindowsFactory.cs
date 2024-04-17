using Constants.WindowsSettings;
using Cysharp.Threading.Tasks;
using Modules.UI.Windows.UnitShop;

namespace Modules.UI
{
    public interface IWindowsFactory
    {
        UniTask<UnitShopWindow> CreateUnitShopWindow();
        void CreateWindow(WindowsNames windowName);
    }
}