using MessagePipe;
using Modules.Entities.Collector.Services;
using Modules.Entities.Generator.Services;
using Modules.Infrastructure;
using Modules.Infrastructure.Services;
using Modules.Signals;
using UnityEngine;
using VContainer.Unity;
using IFactory = Modules.Entities.IFactory;

namespace DI
{
    public class Game : IStartable
    {
        public StateMachine _stateMachine { get; private set; }
        private IGeneratorsService _generatorsService;
        private ICollectorsService _collectorsService;
        private IFactory _factory;
        private ISceneLoader _sceneLoader;
        private readonly IPublisher<LoadingSignal, float> _loadingSignal;

        private Game(IFactory factory, IGeneratorsService generatorsService,
            ICollectorsService collectorsService, ISceneLoader sceneLoader,
            IPublisher<LoadingSignal, float> loadingSignal)
        {
            _sceneLoader = sceneLoader;
            _loadingSignal = loadingSignal;
            _factory = factory;
            _generatorsService = generatorsService;
            _collectorsService = collectorsService;

        }

        public void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            _stateMachine = new StateMachine(_generatorsService, _collectorsService, _factory, _sceneLoader,
                _loadingSignal);
            _stateMachine.SetState(StateMachine.States.BootStrap);
        }
    }
}