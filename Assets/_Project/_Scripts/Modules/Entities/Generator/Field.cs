using Modules.Entities.Generator.Services;
using UnityEngine;
using VContainer;

namespace Modules.Entities.Generator
{
    public class Field : MonoBehaviour
    {
        [Inject] private IGeneratorsService _service;
        public Storage.Storage Storage;
        private void Awake() => _service.AddField(this);
    }
}