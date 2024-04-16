using System;
using Modules.Core;
using Modules.Entities.Converter.ScriptableObjects;
using Modules.Entities.Deliver;
using UnityEngine;

namespace Modules.Entities.Converter
{
    /// <summary>
    /// Object that converts one resource to another. 
    /// </summary>
    public class Converter : GameEntity<Converter.States>, IMoneyActionObject
    {
        public event Action OnMoneyAction;

        [SerializeField] private ConverterResourceData _resourceData;
        [SerializeField] private ConverterSettingsSo Settings;
        [SerializeField] private StorageConnection _storageConnection;

        private ConverterModel _model;
        private IdleProcess<Converter> _converting;
        private IdleProcess<Converter> _loading;

        public enum States
        {
            None,
            Active,
            Inactive
        }

        public void Init(StorageConnection connections, ConverterResourceData resources)
        {
            _storageConnection = connections;
            _resourceData = resources;

            _model = new ConverterModel(Settings);
            _converting = new IdleProcess<Converter>(_model.Speed, this, this);
            _loading = new IdleProcess<Converter>(_model.LoadingTime, this, this);

            CollectProcessWithWait();
        }

        private void CollectProcessWithWait()
        {
            WaitIt(() => _storageConnection.StorageA._model.CurrentLoad.Value > 0, StartProcess);
        }

        private void StartProcess()
        {
            _loading.Start(OnEndLoading);
        }

        private void OnEndLoading()
        {
            SetState(States.Active);
            _storageConnection.StorageA.Collect();
            _converting.Start(ResourceReady);
        }
        private void ResourceReady()
        {
            SetState(States.Inactive);
            OnMoneyAction?.Invoke();
            _storageConnection.StorageB.Add();
            CollectProcessWithWait();
        }

        public override void ChangeState()
        {
        }
    }
}