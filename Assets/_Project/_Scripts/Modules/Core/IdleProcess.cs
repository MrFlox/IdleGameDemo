using System;
using System.Collections;
using UnityEngine;

namespace Modules.Core
{
    public class IdleProcess<TProcessHolerType>
    {
        public bool Active;
        public bool Finished;
        public float Value;
        private readonly MonoBehaviour _corutineProvider;
        private readonly TProcessHolerType _processHolder;
        public event Action<TProcessHolerType> OnStart;
        public event Action<TProcessHolerType> OnEnd;
        private Action _onEndCallback;

        public ReactiveProperty<float> processTime;
        private float _curTime;
        private float m_startTime;
        private float _startTime;

        public IdleProcess(ReactiveProperty<float> processTimeValue, MonoBehaviour corutineProvider,
            TProcessHolerType processHolder)
        {
            _corutineProvider = corutineProvider;
            _processHolder = processHolder;
            processTime = processTimeValue;
        }

        public void Start(Action onEndCallback = null)
        {
            // Debug.Log(_corutineProvider.name + " > StartProcess>>");
            _onEndCallback = onEndCallback;
            Active = true;
            Finished = false;
            OnStart?.Invoke(_processHolder);
            _corutineProvider.StartCoroutine(Process());
        }
        private IEnumerator Process()
        {
            var elapsedTime = 0.0f;

            while (elapsedTime < processTime.Value)
            {
                Value = elapsedTime / processTime.Value;
                yield return null;
                elapsedTime += Time.deltaTime;
            }
            Value = 1.0f;
            Active = false;
            Finished = true;


            OnEnd?.Invoke(_processHolder);
            // Debug.Log(_corutineProvider.name + " > EndProcess>>");
            _onEndCallback?.Invoke();
        }
        public void Clear()
        {
            Active = false;
            Finished = false;
        }
    }
}