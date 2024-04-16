using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Modules.Entities;
using Modules.Entities.Collector.Services;
using Modules.Entities.Generator;
using Modules.Entities.Generator.Services;
using Modules.Entities.Storage;
using UnityEngine;

namespace Modules.Infrastructure.States
{
    public sealed class InitState : IGameState
    {
        private readonly IStateManager _stateMachine;
        private readonly IGeneratorsService _generatorsService;
        private readonly ICollectorsService _collectorsService;
        private readonly IFactory _factory;

        public InitState(
            IStateManager stateMachine,
            IGeneratorsService generatorsService,
            ICollectorsService collectorsService,
            IFactory factory)
        {
            _stateMachine = stateMachine;
            _generatorsService = generatorsService;
            _collectorsService = collectorsService;
            _factory = factory;
        }

        public void EnterState()
        {
            var storage = FindStorageOnLevel();
            _collectorsService.SetStorage(storage);
            AddGenerators(10);

            _stateMachine.SetState(StateMachine.States.Collectors);
        }

        private void AddGenerators(int fieldCount)
        {
            for (var i = 0; i < fieldCount; i++)
                AddGenerator(i);
        }

        private Storage FindStorageOnLevel()
        {
            var obj = GameObject.FindGameObjectWithTag("storage");
            return obj.GetComponent<Storage>();
        }

        private void AddGenerator(int value)
        {
            var generator = _factory.CreateGenerator();
            generator.Init(_generatorsService.GetFreePosition(value));
            _generatorsService.AddGenerator(generator);
            // generator.generationProcess.OnEnd += OnGeneraterReadyToCollectHandler;
            generator.StartGrow();
        }

        private void OnGeneraterReadyToCollectHandler(Generator generator) =>
            generationProcessOnOnEnd(generator);

        private async UniTask generationProcessOnOnEnd(Generator generator)
        {
            generator.generationProcess.OnEnd -= OnGeneraterReadyToCollectHandler;
            var collector = await _collectorsService.CreateCollector();
            collector.SetGenerator(generator);
        }
    }
}