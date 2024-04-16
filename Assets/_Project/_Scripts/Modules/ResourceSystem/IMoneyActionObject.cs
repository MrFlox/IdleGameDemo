using System;

namespace Modules.Entities
{
    public interface IMoneyActionObject
    {
        event Action OnMoneyAction;
    }
}