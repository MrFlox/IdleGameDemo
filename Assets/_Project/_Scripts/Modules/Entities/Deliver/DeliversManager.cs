using System;
using Constants;
using Cysharp.Threading.Tasks;
using Modules.Signals;
using TriInspector;

namespace Modules.Entities.Deliver
{
    public sealed class DeliversManager : AbstractUnitManager<DeliverParking, Deliver>
    {
        protected override void Start()
        {
            base.Start();
            InitAllUnits();
        }

        protected override void OnBuyItemHandler(BuyUnitSignal signal)
        {
            if (signal.UnitType == UnitTypes.Deliver)
            {
                AddUnit();
            }
        }

        [Button]
        public override async void AddUnit()
        {
            var currentConverterIndex = _units.Count;
            if (currentConverterIndex >= _spawners.Count)
                return;

            var deliver = await _spawners[currentConverterIndex].AddDeliver();
            _units.Add(deliver);
        }
    }
}