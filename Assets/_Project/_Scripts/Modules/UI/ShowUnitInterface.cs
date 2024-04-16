using Modules.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Modules.UI
{
    public class ShowUnitInterface : MonoBehaviour
    {
        [SerializeField] private AssetReferenceGameObject windowReference;
        private AssetLoaderService _assetLoaderService;
        private IObjectResolver _container;

        [Inject] private void Construct(AssetLoaderService assetLoaderService, IObjectResolver container)
        {
            _container = container;
            _assetLoaderService = assetLoaderService;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowWindow();
            }
        }

        private async void ShowWindow()
        {
            var window = await _assetLoaderService.LoadAssetReference(windowReference);
            _container.Instantiate(window);
        }
    }
}