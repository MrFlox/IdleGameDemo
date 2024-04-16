using System.Collections;
using UnityEngine;

namespace Modules.Entities.Generator
{
    public class ScaleInTime
    {
        private readonly GameObject _gameObject;
        private readonly float _time;
        private readonly float _startScale;
        private readonly float _endScale;
        private float _value;
        private readonly Vector3 _initialLocalScale;
        public ScaleInTime(ICoroutineProvider coroutineProvider, GameObject gameObject, float time, float startScale,
            float endScale)
        {
            _gameObject = gameObject;
            _time = time;
            _startScale = startScale;
            _endScale = endScale;
            _initialLocalScale = gameObject.transform.localScale;
            coroutineProvider.StartCoroutine(Scale());
        }

        private IEnumerator Scale()
        {
            var elapsedTime = 0.0f;

            while (elapsedTime < _time)
            {
                _value = elapsedTime / _time;
                UpdateScale(_value);
                yield return null;
                elapsedTime += Time.deltaTime;
            }
            _value = 1.0f;
            UpdateScale(_value);
        }
        private void UpdateScale(float value) =>
            _gameObject.transform.localScale = _initialLocalScale * _value;
    }


    public interface ICoroutineProvider
    {
        public Coroutine StartCoroutine(IEnumerator routine);

    }
}