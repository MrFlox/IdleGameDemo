using System.Collections.Generic;
using Modules.Entities;
using Modules.UI.Components;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Modules.UI.Windows.UnitShop
{
    public class UnitShopWindow : AbstractWindow
    {
        [SerializeField] private List<ShopUnitLine> _content;
        [SerializeField] private AssetReferenceGameObject _unitLinePrefabReference;
        [SerializeField] private RectTransform _contentRect;
        [SerializeField] private TabComponent _tabs;

        private AssetLoaderService _assetLoaderService;
        private IUnitShopPresenter _unitShopPresenter;

        [Inject] private void Construct(AssetLoaderService assetLoaderService, IUnitShopPresenter unitShopPresenter)
        {
            _unitShopPresenter = unitShopPresenter;
            _assetLoaderService = assetLoaderService;
        }

        public void Init(IUnitShopPresenter unitShopPresenter)
        {
            _unitShopPresenter = unitShopPresenter;
            AddUnitLines();
        }

        private async void AddUnitLines()
        {
            var unitLinePrefab = await _assetLoaderService.LoadAssetReference(_unitLinePrefabReference);
            var unitLine = unitLinePrefab.GetComponent<BuyUnitComponent>();

            foreach (var unitInfo in _content)
            {
                var line = Instantiate(unitLine, _contentRect, false);
                line.Init(unitInfo, _unitShopPresenter.BuyConverter);
            }
        }
    }
}