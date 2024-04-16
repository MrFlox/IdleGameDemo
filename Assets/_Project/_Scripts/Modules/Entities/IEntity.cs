using System;
using System.Collections;

namespace Modules.Entities
{
    public interface IEntity
    {
        IEnumerator WaitIt(Func<bool> predicate, Action callback);
        void Init();
    }
}