using Modules.Core;

namespace Modules.ResourceSystem
{
    public interface IResourcesService
    {
        void Add(Account.Type type, int value = 1);
        void Spend(Account.Type type, int value = 1);
        ReactiveProperty<int> Get(Account.Type type);
    }
}