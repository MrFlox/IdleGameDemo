using Constants;

namespace Modules.Signals
{

    public record BuyUnitSignal(UnitTypes UnitType)
    {
        public UnitTypes UnitType { get; } = UnitType;
    }
}