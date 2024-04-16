using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.Entities
{
    public interface IFactory
    {
        Generator.Generator CreateGenerator();
        UniTask<Collector.Collector> CreateCollector();
        UniTask<Deliver.Deliver> CreateDeliver();
        UniTask<Converter.Converter> CreateConverter();
        UniTask<GameObject> CreateBucket(AssetReferenceGameObject prefabReference);
    }
}