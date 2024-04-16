using Constants;
using Cysharp.Threading.Tasks;
using Modules.Signals;
using TriInspector;

namespace Modules.Entities.Converter
{
    public class ConverterManager : AbstractUnitManager<ConverterSpawner, Converter>
    {

        protected override void Start()
        {
            base.Start();
            InitAllUnits();
        }

        protected override void OnBuyItemHandler(BuyUnitSignal signal)
        {
            if (signal.UnitType == UnitTypes.Converter)
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

            var converter = await _spawners[currentConverterIndex].SpawnConverter();
            _units.Add(converter);
        }
    }
}