using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Modules.Entities.Collector.ScriptableObject;
using Modules.Entities.Generator.Services;
using UnityEngine;

namespace Modules.Entities.Collector.Services
{
    public class CollectorsService : ICollectorsService
    {
        private IGeneratorsService _generatorsService;
        private readonly CollectorSettingsSo _collectorSettings;
        private List<Collector> _collectors = new();
        private IFactory _factory;
        private Storage.Storage _storage;

        public CollectorsService(
            IGeneratorsService generatorsService,
            CollectorSettingsSo collectorSettings,
            IFactory factory)
        {
            _generatorsService = generatorsService;
            _collectorSettings = collectorSettings;
            _factory = factory;
        }
        public async UniTask<Collector> CreateCollector()
        {
            var newCollector = await _factory.CreateCollector();

            PlaceCollectorToSpawnPoint(newCollector);
            _collectors.Add(newCollector);
            newCollector.SetField(_generatorsService.GetField());
            return newCollector;
        }
        public void SetStorage(Storage.Storage storage) => _storage = storage;
        private void PlaceCollectorToSpawnPoint(Collector newCollector)
        {
            newCollector.transform.position = GameObject.FindGameObjectWithTag("CollectorsSpawn").transform.position;
        }
    }
}