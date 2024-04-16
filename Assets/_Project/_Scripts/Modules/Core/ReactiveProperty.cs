using System;
using UnityEngine;

namespace Modules.Core
{
    [Serializable]
    public class ReactiveProperty<T>
    {
        [SerializeField] private T value;
        private Action<T> _onChanged;

        public ReactiveProperty(T value) => Value = value;
        public void Subscribe(Action<T> onChange) => _onChanged += onChange;
        public void Unsubscribe(Action<T> onChange) => _onChanged -= onChange;

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                _onChanged?.Invoke(value);
            }
        }
    }
}