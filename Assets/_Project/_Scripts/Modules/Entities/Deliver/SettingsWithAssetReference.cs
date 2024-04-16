using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.Entities.Deliver
{
    public abstract class SettingsWithAssetReference : ScriptableObject
    {
        public AssetReferenceGameObject PrefabReference;
    }
}