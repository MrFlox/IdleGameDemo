using System;
using UnityEngine;

namespace Modules.Core
{
    public class IdleModel<TActionType, TSType> where TSType : ScriptableObject
    {
        public event Action<IdleModel<TActionType, TSType>> OnChangeModel;
        public IdleModel(TSType settings) => LoadFromSettings(settings);

        protected virtual void OnChangeHandler<T>(T f) => OnChangeModel?.Invoke(this);
        protected virtual void LoadFromSettings(TSType settings)
        {
            throw new NotImplementedException();
        }
        public void Subscribe(Action<IdleModel<TActionType, TSType>> onChange) => OnChangeModel += onChange;
        public void Unsubscribe(Action<IdleModel<TActionType, TSType>> onChange) => OnChangeModel -= onChange;

    }
}