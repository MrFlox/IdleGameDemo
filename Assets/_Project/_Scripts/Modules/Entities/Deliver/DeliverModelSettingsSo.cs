using UnityEngine;

namespace Modules.Entities.Deliver
{
    [CreateAssetMenu(menuName = "IdleGame/Create DeliverModelSettingsSo", fileName = "DeliverModelSettingsSo",
        order = 0)]
    public class DeliverModelSettingsSo : SettingsWithAssetReference
    {
        public int MaxLoad;
        public float CollectingSpeed;

        [Range(.5f, 5f)]
        public float Speed;
    }
}