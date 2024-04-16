using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MessagePipe;
using Modules.Signals;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace Modules.Entities
{
    public abstract class AbstractUnitManager<TUnitSpawner, TUnitType> : MonoBehaviour
    {
        [FormerlySerializedAs("_spawner")] [FormerlySerializedAs("_converterSpawners")] [SerializeField]
        protected List<TUnitSpawner> _spawners;

        readonly protected List<TUnitType> _units = new();

        private ISubscriber<BuyUnitSignal> _buyUnitSignal;

        [Inject] private void Construct(ISubscriber<BuyUnitSignal> buyUnitSignal)
        {
            _buyUnitSignal = buyUnitSignal;
        }

        protected virtual void Start()
        {
            _buyUnitSignal.Subscribe(OnBuyItemHandler);
        }

        protected async void InitAllUnits(float spawnTimeout = .4f)
        {
            foreach (var spawner in _spawners)
            {
                await UniTask.WaitForSeconds(spawnTimeout);
                AddUnit();
            }
        }

        public virtual async void AddUnit()
        {
        }

        protected virtual void OnBuyItemHandler(BuyUnitSignal signal)
        {
        }
    }
}