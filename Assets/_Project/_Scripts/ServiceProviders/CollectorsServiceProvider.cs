using Modules.Entities.Collector.Services;
using TriInspector;
using UnityEngine;
using VContainer;

namespace ServiceProviders
{
    public class CollectorsServiceProvider : MonoBehaviour
    {
        [Inject] private ICollectorsService _collectorsService;

        [Button]
        private void CreateCollector() => _collectorsService.CreateCollector();
    }
}