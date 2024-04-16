using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using VContainer;

namespace Modules.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Button ShowUnitShop;
        [SerializeField] private AssetReferenceGameObject windowReference;
        private IWindowsFactory _windowsFactory;

        [Inject] private void Construct(IWindowsFactory windowsFactory)
        {
            _windowsFactory = windowsFactory;
        }

        private void OnEnable() =>
            ShowUnitShop.onClick.AddListener(ShowUnitShopHandler);

        private void OnDisable() =>
            ShowUnitShop.onClick.RemoveAllListeners();

        private async void ShowUnitShopHandler()
        {
            await _windowsFactory.CreateUnitShopWindow();
        }
    }
}