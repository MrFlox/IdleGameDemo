using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Modules.Core;
using Modules.ResourceSystem;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Modules.Entities.Storage
{
    [RequireComponent(typeof(Storage))]
    public class StorageView : MonoBehaviour
    {
        [SerializeField] private List<Transform> _bucketPositions = new();

        private List<GameObject> _buckets = new();
        private Storage _storage;
        [Required] public ResourceSo Resource;
        private IFactory _factory;

        [Inject] private void Construct(IFactory factory) => _factory = factory;

        [Button]
        private void InitBucketPositions()
        {
            _bucketPositions.Clear();
            foreach (Transform bucket in transform)
            {
                if (bucket.TryGetComponent<Backet>(out _))
                    _bucketPositions.Add(bucket);
            }
        }

        [Button]
        public async Task SetResource(ResourceSo newResource)
        {
            Resource = newResource;
            await ChangeResourceSkin();
        }

        private async UniTask<GameObject> InstantiateBucket(Vector3 newPosition)
        {
            var bucket = await _factory.CreateBucket(Resource.PrefabReference);
            return Instantiate(bucket, newPosition, Quaternion.identity, transform);
        }

        private async UniTask ChangeResourceSkin()
        {
            _buckets.Clear();

            foreach (var bucketPosition in _bucketPositions)
            {
                var newBucketSkinFromResource = await InstantiateBucket(bucketPosition.position);
                newBucketSkinFromResource.transform.localScale =
                    new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
                _buckets.Add(newBucketSkinFromResource);
            }
        }

        [Button(ButtonSizes.Large)]
        private void HideAllBuckets()
        {
            foreach (var bucket in _buckets)
                bucket.SetActive(false);
        }

        private void Start()
        {
            _storage = GetComponent<Storage>();
            OnInitializedHandler().Forget();
        }

        private async UniTask OnInitializedHandler()
        {
            _storage._model.OnChangeModel += OnChangeModel;
            await SetResource(_storage.Resource);
            ShowCurrentBucketCount();
        }

        private void OnChangeModel(IdleModel<StorageModel, StorageSettingsSo> obj) =>
            ShowCurrentBucketCount();

        [Button(ButtonSizes.Large)]
        private void ShowBucketCount(int count = 0)
        {
            if (count > _buckets.Count)
            {
                Debug.LogError("Not enught space in storage", this);
                return;
            }
            HideAllBuckets();
            for (var i = 0; i < count; i++)
                _buckets[i].SetActive(true);
        }

        private void ShowCurrentBucketCount() => ShowBucketCount(_storage._model.CurrentLoad.Value);
    }
}