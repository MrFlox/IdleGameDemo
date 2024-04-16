using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Modules.UI
{
    public class FlyController : MonoBehaviour
    {
        public event Action<FlyController> OnComplete;

        private const float FlyingTime = 3f;
        [SerializeField] private TMP_Text text;
        private void OnEnable() => StartCoroutine(Go());

        private IEnumerator Go()
        {
            var startTime = Time.time;
            while (Time.time - startTime < FlyingTime)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
                yield return null;
            }
            OnComplete?.Invoke(this);
        }
        public void SetText(int moneyPerTick) => text.text = "+" + moneyPerTick;
    }
}