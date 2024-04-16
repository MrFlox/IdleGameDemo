using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Modules.Entities.Services
{
    public class AbstractFactory<T> : IAbstractFactory<T> where T : MonoBehaviour
    {
        private readonly IObjectResolver _container;
        private List<T> _variants;
        private ObjectPool<T> _pool;

        public AbstractFactory(IObjectResolver container) => _container = container;

        public void Init(List<T> variants, int defaultCapacity = 15)
        {
            _variants = variants;
            _pool = new ObjectPool<T>(CreateItem,
                OnTakeItemFromPool,
                OnReturnItem,
                OnDestroyItem,
                true, defaultCapacity, defaultCapacity * 10);
        }

        private T CreateItem() =>
            _container.Instantiate(_variants[Random.Range(0, _variants.Count)]);

        private static void OnTakeItemFromPool(T item) => item.gameObject.SetActive(true);

        private static void OnReturnItem(T item) => item.gameObject.SetActive(false);

        private static void OnDestroyItem(T item) => Object.Destroy(item.gameObject);

        public T Create() => _pool.Get();

        public void Release(T item) => _pool.Release(item);
    }
}