using System.Collections.Generic;
using System.Linq;
using Modules.Entities.SharedComponents;
using Modules.ResourceSystem;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.Entities.Generator.ScriptableObjects
{
    [CreateAssetMenu(menuName = "IdleGame/Create GeneratorsSettingsSo", fileName = "GeneratorsSettingsSo", order = 0)]
    public class GeneratorsSettingsSo : ScriptableObject
    {
        [FormerlySerializedAs("storage")] public Vector3 Storage;
        public List<Vector3> GenPositions = new();
        public float GenerationSpeed;

        public int ValuePerCircle;

        public float StartGrowDelay = .8f;
        public ShadowScript shadowPrefab;

        public ResourceSo Resource;

        [Button]
        public void InitGenPositions()
        {
            GenPositions.Clear();
            var re = GameObject.FindGameObjectsWithTag("BushSpownPoint").ToList();
            foreach (var pos in re)
                GenPositions.Add(pos.transform.position);
        }
    }
}