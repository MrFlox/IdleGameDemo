using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Modules.Entities.Collector.ScriptableObject;
using Modules.Entities.Converter.ScriptableObjects;
using Modules.Entities.Deliver;
using Modules.Entities.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Modules.Entities
{

    public class Factory : IFactory
    {
        private readonly FactorySettingsSo _settings;
        private readonly IObjectResolver _container;
        private readonly AssetLoaderService _assetLoader;
        private readonly CollectorSettingsSo _collectorSettings;
        private readonly DeliverModelSettingsSo _deliverSettings;
        private readonly ConverterSettingsSo _converterSettings;

        public Factory(
            FactorySettingsSo settings,
            IObjectResolver container,
            AssetLoaderService assetLoader,
            CollectorSettingsSo collectorSettings,
            DeliverModelSettingsSo deliverSettings,
            ConverterSettingsSo converterSettings
        )
        {
            _settings = settings;
            _container = container;
            _assetLoader = assetLoader;
            _collectorSettings = collectorSettings;
            _deliverSettings = deliverSettings;
            _converterSettings = converterSettings;
        }

        private async UniTask<T> Load<T>(SettingsWithAssetReference settings)
        {
            var loadAssetReference =
                await _assetLoader.LoadAssetReference(settings.PrefabReference);
            var instance = _container.Instantiate(loadAssetReference);
            var result = instance.GetComponent<T>();
            return result;
        }

        public async UniTask<GameObject> CreateBucket(AssetReferenceGameObject prefabReference)
        {
            var loadAssetReference =
                await _assetLoader.LoadAssetReference(prefabReference);
            return loadAssetReference;
        }

        public Generator.Generator CreateGenerator()
        {
            var generator = _container
                .Instantiate(_settings.generatorPrefab)
                .GetComponent<Generator.Generator>();

            return generator;
        }

        public async UniTask<Deliver.Deliver> CreateDeliver() =>
            await Load<Deliver.Deliver>(_deliverSettings);

        public async UniTask<Converter.Converter> CreateConverter() =>
            await Load<Converter.Converter>(_converterSettings);

        public async UniTask<Collector.Collector> CreateCollector()
        {
            var collector = await Load<Collector.Collector>(_collectorSettings);
            collector.Init(_collectorSettings);
            return collector;
        }
    }
}