using System;
using Constants.WindowsSettings;
using Cysharp.Threading.Tasks;
using Modules.Entities;
using Modules.UI.Windows.UnitShop;
using VContainer;
using VContainer.Unity;

namespace Modules.UI
{
    public class WindowsFactory : IWindowsFactory
    {
        private const string UnitShopWindow = "UI/Windows/MachinesAndVehicles/Machines_Vehicles.prefab";
        private const string UnitUpgrade = "UI/Windows/UpgradeUnit/UpgradeUnit.prefab";

        private readonly AssetLoaderService _assetLoaderService;
        private readonly IObjectResolver _container;
        private readonly IUnitShopPresenter _unitShopPresenter;

        public WindowsFactory(AssetLoaderService assetLoaderService,
            IObjectResolver container,
            IUnitShopPresenter unitShopPresenter)
        {
            _assetLoaderService = assetLoaderService;
            _container = container;
            _unitShopPresenter = unitShopPresenter;
        }

        public async void CreateWindow(WindowsNames windowName)
        {
            switch (windowName)
            {
                case WindowsNames.UnitUpgrade:
                    await CreateUnitUpgradeWindow();
                    break;
                case WindowsNames.CollectorAndFieldsShop:
                    await CreateUnitShopWindow();
                    break;
                case WindowsNames.DeliverAndMachinesShop:
                    break;
                case WindowsNames.Statistics:
                    break;
                case WindowsNames.Settings:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(windowName), windowName, null);
            }
        }

        public async UniTask<UnitShopWindow> CreateUnitShopWindow()
        {
            var windowPrefab = await _assetLoaderService.LoadAssetReference(UnitShopWindow);
            var unitShopInstance = _container.Instantiate(windowPrefab);
            var unityShopWindow = unitShopInstance.transform.GetChild(0).GetComponent<UnitShopWindow>();
            unityShopWindow.Init(_unitShopPresenter);
            return unityShopWindow;
        }

        private async UniTask CreateUnitUpgradeWindow()
        {
            var window = await _assetLoaderService.LoadAssetReference(UnitUpgrade);
            _container.Instantiate(window);
        }
    }
}