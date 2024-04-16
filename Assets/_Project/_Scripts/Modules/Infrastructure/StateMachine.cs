using System;
using System.Collections.Generic;
using MessagePipe;
using Modules.Entities;
using Modules.Entities.Collector.Services;
using Modules.Entities.Generator.Services;
using Modules.Infrastructure.Services;
using Modules.Infrastructure.States;
using Modules.Signals;
using TriInspector;

namespace Modules.Infrastructure
{
    public class StateMachine : IStateManager
    {
        public event Action<States> OnChangeState;
        private readonly IGeneratorsService _generatorsService;
        private readonly ICollectorsService _collectorsService;
        private Dictionary<States, IGameState> _states;
        private States _currentState = States.None;
        private IFactory _factory;
        private readonly ISceneLoader _sceneLoader;
        public enum States
        {
            None,
            BootStrap,
            LoadLevel,
            Init,
            Collectors
        }
        public StateMachine(
            IGeneratorsService generatorsService,
            ICollectorsService collectorsService,
            IFactory factory,
            ISceneLoader sceneLoader,
            IPublisher<LoadingSignal, float> loadingSignal
        )
        {
            _generatorsService = generatorsService;
            _collectorsService = collectorsService;
            _factory = factory;
            _sceneLoader = sceneLoader;

            _states = new Dictionary<States, IGameState>()
            {
                [States.BootStrap] = new BootStrapState(this),
                [States.LoadLevel] = new LoadLevelState(this, loadingSignal),
                [States.Init] = new InitState(this, _generatorsService, _collectorsService, _factory),
                [States.Collectors] = new AddCollectorsState(_collectorsService, _generatorsService)
            };
        }
        [Button]
        public void SetState(States newState)
        {
            if (_currentState == newState) return;
            _currentState = newState;
            OnChangeState?.Invoke(_currentState);
            _states[newState].EnterState();
            // Debug.Log("New State: " + _currentState);
        }
    }
}