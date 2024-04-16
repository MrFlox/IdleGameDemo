using System;
using Modules.Core;
using Modules.Entities.Generator.ScriptableObjects;
using static Modules.Utils.Utils;

namespace Modules.Entities.Generator
{
    [Serializable]
    public class GeneratorModel : IdleModel<GeneratorModel, GeneratorsSettingsSo>
    {
        public ReactiveProperty<float> GenerationSpeed;
        public ReactiveProperty<int> ValuePerCircle;
        public ReactiveProperty<int> CurrentValue;

        public GeneratorModel(GeneratorsSettingsSo settings) : base(settings)
        {
        }
        protected override void LoadFromSettings(GeneratorsSettingsSo settings)
        {
            (ValuePerCircle = FromValue(settings.ValuePerCircle)).Subscribe(OnChangeHandler);
            (GenerationSpeed = FromValue(settings.GenerationSpeed)).Subscribe(OnChangeHandler);
            (CurrentValue = FromValue(0)).Subscribe(OnChangeHandler);
        }
    }
}