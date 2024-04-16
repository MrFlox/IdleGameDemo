using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;
using VContainer.Unity;

namespace Modules.Entities
{
    public class AssetLoaderService
    {
        private readonly IObjectResolver _container;
        public AssetLoaderService(IObjectResolver container) => _container = container;

        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

        public async UniTask<GameObject> LoadAssetReference(string reference)
        {
            if (_completedCache.TryGetValue(reference, out var completedHandle))
            {
                return (GameObject)completedHandle.Result;
            }

            var handle = Addressables.LoadAssetAsync<GameObject>(reference);

            handle.Completed += h =>
                _completedCache[reference] = h;

            if (!_handles.TryGetValue(reference, out var resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[reference] = resourceHandles;
            }
            resourceHandles.Add(handle);

            await handle.Task;
            return handle.Result;
        }

        public async UniTask<GameObject> LoadAssetReference(AssetReferenceGameObject reference)
        {
            if (_completedCache.TryGetValue(reference.AssetGUID, out var completedHandle))
            {
                return (GameObject)completedHandle.Result;
            }
            var handle = Addressables.LoadAssetAsync<GameObject>(reference);

            handle.Completed += h =>
                _completedCache[reference.AssetGUID] = h;

            if (!_handles.TryGetValue(reference.AssetGUID, out var resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[reference.AssetGUID] = resourceHandles;
            }
            resourceHandles.Add(handle);

            await handle.Task;
            return handle.Result;
        }
        public async UniTask<GameObject> Load(AssetReferenceGameObject reference)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(reference);
            await handle.ToUniTask();
            var prefab = handle.Result;

            if (prefab != null)
            {
                var instance = _container.Instantiate(prefab);
                if (instance != null)
                {
                    Addressables.Release(handle);
                    return instance;
                }
            }
            else
            {
                Debug.LogError("Error loading prefab");
            }

            Addressables.Release(handle);
            return null;
        }

        public async UniTask<TType> LoadAssetReference<TType>(AssetReferenceGameObject reference)
            where TType : MonoBehaviour
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(reference);
            await handle.ToUniTask();
            var prefab = handle.Result;

            if (prefab != null)
            {
                var instance = _container.Instantiate(prefab);
                var collectorComponent = instance.GetComponent<TType>();
                if (collectorComponent != null)
                {
                    Addressables.Release(handle);
                    return collectorComponent;
                }
            }
            else
            {
                Debug.LogError("Error loading prefab");
            }

            Addressables.Release(handle);
            return null;
        }


        public async UniTask<TType> LoadPrefabAsync<TType>(string key, Factory factory) where TType : MonoBehaviour
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(key);
            await handle.ToUniTask();
            var prefab = handle.Result;

            if (prefab != null)
            {
                var instance = _container.Instantiate(prefab);
                var collectorComponent = instance.GetComponent<TType>();
                if (collectorComponent != null)
                {
                    Addressables.Release(handle);
                    return collectorComponent;
                }
            }
            else
            {
                Debug.LogError("Error loading prefab");
            }

            Addressables.Release(handle);
            return null;
        }
    }
}