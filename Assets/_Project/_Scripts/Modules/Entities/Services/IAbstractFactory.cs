using System.Collections.Generic;

namespace Modules.Entities.Services
{
    public interface IAbstractFactory<T>
    {
        T Create();
        void Release(T item);
        void Init(List<T> variants, int defaultCapacity = 15);
    }
}