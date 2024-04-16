using TriInspector;
using UnityEngine;

namespace Modules.Entities.ScriptableObjects
{
    [CreateAssetMenu(menuName = "IdleGame/Create FactorySettingsSo", fileName = "FactorySettingsSo", order = 0)]
    public class FactorySettingsSo : ScriptableObject
    {
        public GameObject generatorPrefab;
        public GameObject collectorPrefab;
        public Vector3 genetorsCenterPosition;

        [Button]
        private void GetPosition(Transform transform)
        {
            genetorsCenterPosition = transform.position;
        }
    }
}