using System;
using System.Collections;
using TriInspector;
using UnityEngine;

namespace Modules.Entities
{
    public class GameEntity<TStateType> : MonoBehaviour, IGameEntity<TStateType>
    {
        [ReadOnly] [SerializeField] protected TStateType _state;

        public event Action<TStateType> OnChangeState;

        [Button]
        public void SetState(TStateType newState)
        {
            if (_state.Equals(newState)) return;
            _state = newState;
            ChangeState();
            OnChangeState?.Invoke(newState);
        }
        protected void WaitIt(Func<bool> predicate, Action callback) =>
            StartCoroutine(WaitingProcess(predicate, callback));

        private IEnumerator WaitingProcess(Func<bool> predicate, Action callback)
        {
            yield return new WaitUntil(predicate);
            callback?.Invoke();
            yield return null;
        }

        public virtual void ChangeState() =>
            throw new NotImplementedException();
    }

    public interface IGameEntity<TStateType>
    {
        void SetState(TStateType newState);
    }
}