using System;
using Modules.Core;
using Modules.Entities.SharedComponents;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Modules.Entities.Deliver
{
    /// <summary>
    /// Unit that delivers resources from one Storage to another
    /// </summary>
    [SelectionBase]
    [RequireComponent(typeof(MovingObject))]
    public class Deliver : GameEntity<Deliver.States>
    {
        [SerializeField] private DeliverModel model;

        public StorageConnection StorageConnection;

        private DeliverModelSettingsSo _settings;
        private IdleProcess<Deliver> _processLoading;
        private IdleProcess<Deliver> _processUnloading;
        private MovingObject _moving;

        [Inject] private void Construct(DeliverModelSettingsSo settings) =>
            _settings = settings;

        public enum States
        {
            None,
            Idle,
            GoToStorageA,
            Load,
            GoToStorageB,
            Unload
        }

        public void Init()
        {
            model = new DeliverModel(_settings);
            _processLoading = new IdleProcess<Deliver>(model.CollectingSpeed, this, this);
            _processUnloading = new IdleProcess<Deliver>(model.CollectingSpeed, this, this);
            WaitIt(() => StorageConnection.StorageA._model.CurrentLoad.Value > 0, () => SetState(States.GoToStorageA));
            _moving = GetComponent<MovingObject>();
            _moving.SetSpeed(model.MovingSpeed.Value);
        }

        public override void ChangeState()
        {
            switch (_state)
            {
                case States.Load:
                    LoadCargo();
                    break;
                case States.Unload:
                    UnloadCargo();
                    break;
                case States.GoToStorageA:
                    GoToStorageA();
                    break;
                case States.GoToStorageB:
                    GoToStorageB();
                    break;
                case States.Idle:
                case States.None:
                    break;
            }
        }

        [Button]
        private void GoToStorageA() =>
            _moving.GoTo(StorageConnection.StorageA.transform.position, LoadCargo);

        [Button]
        private void GoToStorageB() =>
            _moving.GoTo(StorageConnection.StorageB.transform.position, UnloadCargo);

        private void UnloadCargo() =>
            _processUnloading.Start(OnEndUnloading);

        [Button]
        private void LoadCargo() =>
            WaitForCargo(StorageConnection.StorageA, () => _processLoading.Start(OnEndLoading));

        private void OnEndUnloading()
        {
            StorageConnection.StorageB.Add();
            model.Copacity.Value -= 1;
            SetState(States.GoToStorageA);
        }

        private void OnEndLoading()
        {
            StorageConnection.StorageA.Collect();
            model.Copacity.Value += 1;
            WaitForEmptySpace(StorageConnection.StorageB, () => SetState(States.GoToStorageB));
        }

        private void WaitForCargo(Storage.Storage storage, Action action)
        {
            SetState(States.Idle);
            WaitIt(() => storage._model.CurrentLoad.Value > 0,
                action);
        }

        private void WaitForEmptySpace(Storage.Storage storage, Action action)
        {
            SetState(States.Idle);
            WaitIt(() => storage._model.CurrentLoad.Value < storage._model.MaxCopacity.Value,
                action);
        }
    }
}