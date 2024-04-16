using Constants;
using MessagePipe;
using Modules.Signals;

namespace Modules.UI.Windows.UnitShop
{
    public class UnitShopPresenter : IUnitShopPresenter
    {
        private readonly IPublisher<BuyUnitSignal> _buyUnitSignal;

        public UnitShopPresenter(IPublisher<BuyUnitSignal> buyUnitSignal)
        {
            _buyUnitSignal = buyUnitSignal;
        }

        public void BuyConverter()
        {
            _buyUnitSignal.Publish(new BuyUnitSignal(UnitTypes.Converter));
        }

        public void BuyDeliver()
        {
            _buyUnitSignal.Publish(new BuyUnitSignal(UnitTypes.Deliver));
        }
    }
}