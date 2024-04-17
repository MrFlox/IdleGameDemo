using System.Collections.Generic;
using MessagePipe;
using Modules.Entities;
using Modules.Entities.Collector.Services;
using Modules.Entities.Generator.Services;
using Modules.Infrastructure.States;
using Modules.Signals;
using TriInspector;

namespace Modules.Infrastructure
{
    public class StateMachine : IStateManager
    {
        private readonly IGeneratorsService _generatorsService;
        private readonly ICollectorsService _collectorsService;
        private Dictionary<States, IGameState> _states;
        private States _currentState = States.None;
        private IFactory _factory;

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
            IPublisher<LoadingSignal, float> loadingSignal
        )
        {
            _generatorsService = generatorsService;
            _collectorsService = collectorsService;
            _factory = factory;

            _states = new Dictionary<States, IGameState>()
            {
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
            _states[newState].EnterState();
        }
    }
}