using System.Collections.Generic;
using Modules.Core;

namespace Modules.ResourceSystem
{
    public class ResourcesService : IResourcesService
    {
        public Dictionary<Account.Type, Account> resources;

        public ResourcesService()
        {
            resources = new Dictionary<Account.Type, Account>
            {
                [Account.Type.Money] = new(Account.Type.Money),
                [Account.Type.Wine] = new(Account.Type.Wine)
            };
            // Debug.Log("ResourcesService INITED");
        }

        public void Add(Account.Type type, int value = 1) =>
            resources[type].Add(value);

        public void Spend(Account.Type type, int value = 1) =>
            resources[type].Spend(value);
        public ReactiveProperty<int> Get(Account.Type type) => resources[type].Amount;
    }
}