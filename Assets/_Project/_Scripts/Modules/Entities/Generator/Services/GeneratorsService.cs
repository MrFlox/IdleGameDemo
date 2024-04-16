using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.Entities.Generator.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.Entities.Generator.Services
{
    public class GeneratorsService : IGeneratorsService
    {
        private readonly GeneratorsSettingsSo _settings;
        private List<Vector3> positions;
        private readonly List<Generator> _generators = new();
        private Field _field;

        public Vector3 GetStoragePosition()
        {
            var posObj = GameObject.FindWithTag("PlaceForUnload");
            var pos1 = posObj.transform.GetChild(0).transform.position;
            var pos2 = posObj.transform.GetChild(1).transform.position;

            var randomPoint = Vector3.Lerp(pos1, pos2, Random.value);

            return randomPoint;
        }
        public Transform GetStorageTransform() => throw new NotImplementedException();
        public Vector3 GetFreePosition(int value) => positions[value];
        public int GetPointsCount() => positions.Count;
        public void AddField(Field field) => _field = field;
        public Field GetField() => _field;
        public GeneratorsSettingsSo GetSettings() => _settings;
        public GeneratorsService(GeneratorsSettingsSo settings)
        {
            _settings = settings;
            positions = new List<Vector3>(_settings.GenPositions);
        }
        public Vector3 GetFreeBushPosition() =>
            GetFreeGenerator().transform.position;

        public void AddGenerator(Generator generator) =>
            _generators.Add(generator);
        public void SetCollectorToBush(Generator generator) =>
            generator.IsFree = false;

        [CanBeNull] public Generator GetFreeGenerator() =>
            _generators.FirstOrDefault(g => g.ReadyToCollect());
    }
}