using System;
using Modules.Core;
using static Modules.Utils.Utils;

namespace Modules.Entities.Deliver
{
    [Serializable]
    public class DeliverModel : IdleModel<DeliverModel, DeliverModelSettingsSo>
    {
        public DeliverModel(DeliverModelSettingsSo settings) : base(settings)
        {
        }
        public ReactiveProperty<int> Copacity;
        public ReactiveProperty<int> MaxLoad;
        public ReactiveProperty<float> CollectingSpeed;
        public ReactiveProperty<float> MovingSpeed;

        protected override void LoadFromSettings(DeliverModelSettingsSo settings)
        {
            (Copacity = FromValue(0)).Subscribe(OnChangeHandler);
            (MaxLoad = FromValue(settings.MaxLoad)).Subscribe(OnChangeHandler);
            (CollectingSpeed = FromValue(settings.CollectingSpeed)).Subscribe(OnChangeHandler);
            (MovingSpeed = FromValue(settings.Speed)).Subscribe(OnChangeHandler);
        }
    }
}