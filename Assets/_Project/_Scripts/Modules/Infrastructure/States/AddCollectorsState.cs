using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Modules.Entities.Collector.Services;
using Modules.Entities.Generator.Services;

namespace Modules.Infrastructure.States
{
    public sealed class AddCollectorsState : IGameState
    {
        private const int CollectorCount = 4;
        private readonly ICollectorsService _collectorsService;
        private readonly IGeneratorsService _generatorsService;

        public AddCollectorsState(ICollectorsService collectorsService, IGeneratorsService generatorsService)
        {
            _collectorsService = collectorsService;
            _generatorsService = generatorsService;
        }
        public void EnterState() => AddCollectors(CollectorCount);
        private void AddCollectors(int collectorCount)
        {
            for (var i = 0; i < collectorCount; i++)
                AddCollector();
        }

        private async UniTask AddCollector()
        {
            var collector = await _collectorsService.CreateCollector();
            collector.SetGenerator(_generatorsService.GetFreeGenerator());
            collector.InitBehaviorTree();
        }
    }
}