using Cysharp.Threading.Tasks;

namespace Modules.Entities.Collector.Services
{
    public interface ICollectorsService
    {
        UniTask<Collector> CreateCollector();
        void SetStorage(Storage.Storage storage);
    }
}