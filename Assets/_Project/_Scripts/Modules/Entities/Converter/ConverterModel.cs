using Modules.Core;
using Modules.Entities.Converter.ScriptableObjects;

namespace Modules.Entities.Converter
{

    public class ConverterModel : IdleModel<ConverterModel, ConverterSettingsSo>
    {
        public ConverterModel(ConverterSettingsSo settings) : base(settings)
        {
        }
        public ReactiveProperty<float> Speed { get; set; }
        public ReactiveProperty<float> LoadingTime { get; set; }
        public ReactiveProperty<float> UnloadingTime { get; set; }

        protected override void LoadFromSettings(ConverterSettingsSo settings)
        {
            (Speed = new ReactiveProperty<float>(5f)).Subscribe(OnChangeHandler);
            (LoadingTime = new ReactiveProperty<float>(1f)).Subscribe(OnChangeHandler);
            (UnloadingTime = new ReactiveProperty<float>(1f)).Subscribe(OnChangeHandler);
        }


    }
}