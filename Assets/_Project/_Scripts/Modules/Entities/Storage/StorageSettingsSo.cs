using Modules.ResourceSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.Entities.Storage
{
    [CreateAssetMenu(menuName = "IdleGame/Create StorageModelSo", fileName = "StorageModelSo", order = 0)]
    internal class StorageSettingsSo : ScriptableObject
    {
        [FormerlySerializedAs("maxCopacity")] public int MaxCopacity;

        public ResourceSo Resource;
    }
}