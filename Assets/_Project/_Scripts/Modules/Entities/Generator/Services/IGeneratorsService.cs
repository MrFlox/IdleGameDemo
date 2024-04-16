using Modules.Entities.Generator.ScriptableObjects;
using UnityEngine;

namespace Modules.Entities.Generator.Services
{
    public interface IGeneratorsService
    {
        Vector3 GetFreeBushPosition();
        void AddGenerator(Generator generator);
        void SetCollectorToBush(Generator generator);
        Generator GetFreeGenerator();
        Vector3 GetStoragePosition();
        Vector3 GetFreePosition(int value);
        int GetPointsCount();
        void AddField(Field field);
        Field GetField();
        GeneratorsSettingsSo GetSettings();
        Transform GetStorageTransform();
    }
}