using System;
using Modules.ResourceSystem;
using UnityEngine;

namespace Modules.Entities.Storage
{
    [SelectionBase]
    public class Storage : MonoBehaviour
    {
        [SerializeField] private StorageSettingsSo settings;
        [SerializeField] internal StorageModel _model;
        public ResourceSo Resource;
        public event Action OnInitialized;
        private void Awake() => Init();

        private void Init()
        {
            _model = new StorageModel(settings);
        }
        
        public void Add(int value = 1) =>
            _model.CurrentLoad.Value += value;
        
        public void Collect(int value = 1) =>
            _model.CurrentLoad.Value -= value;
        
        public bool NotFull() =>
            _model.CurrentLoad.Value < _model.MaxCopacity.Value;
    }
}