using Modules.Entities.Deliver;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Modules.Entities.Collector.ScriptableObject
{
    [CreateAssetMenu(menuName = "IdleGame/CollectorSettingsSo", fileName = "CollectorSettings", order = 0)]
    public class CollectorSettingsSo : SettingsWithAssetReference
    {
        public int MaxLoad;
        public float CollectingSpeed;
    }
}