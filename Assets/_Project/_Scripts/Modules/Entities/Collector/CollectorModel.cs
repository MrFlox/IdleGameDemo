using System;
using Modules.Core;
using Modules.Entities.Collector.ScriptableObject;
using static Modules.Utils.Utils;

namespace Modules.Entities.Collector
{

    [Serializable]
    public class CollectorModel : IdleModel<CollectorModel, CollectorSettingsSo>
    {
        public ReactiveProperty<int> Copacity;
        public ReactiveProperty<int> MaxLoad;
        public ReactiveProperty<float> CollectingSpeed;
        public CollectorModel(CollectorSettingsSo settings) : base(settings)
        {
        }

        protected override void LoadFromSettings(CollectorSettingsSo settings)
        {
            (Copacity = FromValue(0)).Subscribe(OnChangeHandler);
            (MaxLoad = FromValue(settings.MaxLoad)).Subscribe(OnChangeHandler);
            (CollectingSpeed = FromValue(settings.CollectingSpeed)).Subscribe(OnChangeHandler);
        }
    }
}