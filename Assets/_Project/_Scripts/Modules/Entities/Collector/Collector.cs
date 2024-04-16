using System;
using Modules.BehaviorTree;
using Modules.Core;
using Modules.Entities.Collector.BTreeNodes;
using Modules.Entities.Collector.ScriptableObject;
using Modules.Entities.Generator;
using Modules.Entities.Generator.Services;
using Modules.Entities.SharedComponents;
using UnityEngine;
using VContainer;

namespace Modules.Entities.Collector
{
    /// <summary>
    /// Represents a unit that collects resources from resource generators.
    /// For example, it could be workers collecting grapes from bushes.
    /// </summary>
    [SelectionBase]
    [RequireComponent(typeof(MovingObject))]
    public sealed class Collector : GameEntity<Collector.States>, IMoneyActionObject
    {
        public event Action OnMoneyAction;
        [SerializeField] private CollectorModel model;
        private IdleProcess<Collector> _collectingProcess;
        private IdleProcess<Collector> _unloadingProcess;
        private MovingObject _moving;
        private IGeneratorsService _generatorsService;
        private Generator.Generator _activeGenerator;
        private Field _field;
        private string _entityName;
        private BTNode _behaviorTree;

        public enum States
        {
            None,
            Idle,
            Walk,
            Work
        }

        [Inject]
        public void Construct(IGeneratorsService generatorsService) =>
            _generatorsService = generatorsService;
        public void Init(CollectorSettingsSo settings)
        {
            model = new CollectorModel(settings);
            _collectingProcess = new IdleProcess<Collector>(model.CollectingSpeed, this, this);
            _unloadingProcess = new IdleProcess<Collector>(model.CollectingSpeed, this, this);
            _entityName = name;
            _moving = GetComponent<MovingObject>();
        }
        public override void ChangeState() => name = _entityName + "> " + _state;

        public void SetField(Field newField) => _field = newField;

        public void SetGenerator(Generator.Generator freeGenerator)
        {
            _activeGenerator = freeGenerator;
            freeGenerator.IsFree = false;
            SetState(States.Walk);
        }

        public void InitBehaviorTree()
        {
            var checkHarvestAvailable = new CheckHarvestAvailable(_activeGenerator);
            var moveToGenerator = new MoveToTarget(_moving, GetActiveGenerator, this);
            var harvest = new Harvest(_activeGenerator, model, _collectingProcess, this);
            var checkReadyToUnload = new CheckReadyToUnload(_field.Storage, model);
            var moveToStorage = new MoveToTarget(_moving, _generatorsService.GetStoragePosition, this);
            var unload = new Unload(_field, _unloadingProcess, this, model);

            var harvestSequence = new Sequence(
                checkHarvestAvailable,
                moveToGenerator,
                harvest
            );

            var unloadSequence = new Sequence(
                checkReadyToUnload,
                moveToStorage,
                unload
            );
            var wait = new Wait(this);

            _behaviorTree = new Selector(
                new Sequence(harvestSequence, unloadSequence),
                wait
            );
        }

        private Transform GetActiveGenerator() => _activeGenerator.transform;

        private void Update() => _behaviorTree?.Evaluate();

        public void Harvest() => OnMoneyAction?.Invoke();
    }
}