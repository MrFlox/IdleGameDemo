using System;
using Modules.Core;
using Modules.Entities.Generator.Services;
using TriInspector;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Modules.Entities.Generator
{
    [SelectionBase]
    public class Generator : MonoBehaviour
    {
        private const float YRange = 0.8f;
        public event Action OnInitialized;
        public event Action OnCollected;
        public GeneratorModel model;
        public bool IsFree = true; //incapsulate
        // private bool _isFree { get; set; }

        public IdleProcess<Generator> generationProcess;
        private IGeneratorsService _service;

        [Inject]
        private void Construct(IGeneratorsService service) => _service = service;
        public void Init(Vector3 newPosition)
        {
            newPosition = GetRandomYPosition(newPosition);
            transform.position = newPosition;
            model = new GeneratorModel(_service.GetSettings());
            generationProcess = new IdleProcess<Generator>(model.GenerationSpeed, this, this);
            //-------------------
            OnInitialized?.Invoke();
        }
        private static Vector3 GetRandomYPosition(Vector3 newPosition)
        {
            newPosition.y += Random.Range(-YRange, YRange); //todo: move to another component
            return newPosition;
        }

        public int CurrentValue
        {
            get => model.CurrentValue.Value;
            set { model.CurrentValue.Value = value; }
        }

        public int ValuePerCircle => model.ValuePerCircle.Value;

        [Button]
        public void StartGrow() => generationProcess.Start(OnEndGeneration);
        public bool ReadyToCollect() => IsFree;
        public void Collect(int value = 1)
        {
            CurrentValue -= value;
            OnCollected?.Invoke();
            if (CurrentValue <= 0)
                StartGrow();
        }
        private void OnEndGeneration() => CurrentValue += ValuePerCircle;
        public bool IsReadyForHarvest() => model.CurrentValue.Value > 0;
    }
}