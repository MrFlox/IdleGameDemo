using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Modules.Entities.Deliver;
using UnityEngine;
using VContainer;

namespace Modules.Entities.Converter
{
    [SelectionBase]
    public class ConverterSpawner : MonoBehaviour
    {
        [SerializeField] private StorageConnection _storageConnection;
        [SerializeField] private ConverterResourceData _resourceData;

        private IFactory _factory;

        [Inject] private void Construct(IFactory factory)
        {
            _factory = factory;
        }

        private void Start()
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        private void OnMouseDown() => SpawnConverter().Forget();

        public async UniTask<Converter> SpawnConverter()
        {
            var converter = await _factory.CreateConverter();
            converter.transform.position = transform.position;
            converter.transform.rotation = transform.rotation;
            converter.Init(_storageConnection, _resourceData);
            converter.transform.localScale = new Vector3(0, 0, 0);
            converter.transform.DOScale(new Vector3(1, 1, 1), 1).SetEase(Ease.OutElastic);
            return converter;
        }
    }
}