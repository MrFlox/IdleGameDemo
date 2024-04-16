using System;
using Modules.Core;
using static Modules.Utils.Utils;

namespace Modules.Entities.Storage
{
    [Serializable]
    internal class StorageModel : IdleModel<StorageModel, StorageSettingsSo>
    {
        public ReactiveProperty<int> MaxCopacity;
        public ReactiveProperty<int> CurrentLoad;
        public StorageModel(StorageSettingsSo settings) : base(settings)
        {
        }

        protected override void LoadFromSettings(StorageSettingsSo settings)
        {
            (MaxCopacity = FromValue(settings.MaxCopacity)).Subscribe(OnChangeHandler);
            (CurrentLoad = FromValue(0)).Subscribe(OnChangeHandler);
        }
    }
}