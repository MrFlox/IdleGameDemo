using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.ResourceSystem
{
    [CreateAssetMenu(fileName = "Res", menuName = "IdleGame/Resource", order = 0)]
    public class ResourceSo : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Color _color;
        public AssetReferenceGameObject PrefabReference;
    }
}