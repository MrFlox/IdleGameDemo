using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Entities.Generator
{
    [RequireComponent(typeof(Generator))]
    public sealed class GeneratorView : MonoBehaviour, ICoroutineProvider
    {
        [SerializeField] private List<GameObject> berries;
        private List<Vector3> berriesScales = new();
        private Generator _generator;
        private float _value;

        private void Awake()
        {
            InitBerries();
            _generator = GetComponent<Generator>();
            _generator.OnInitialized += OnGenInitHandler;
        }

        private void InitBerries()
        {
            foreach (var berry in berries)
                berriesScales.Add(berry.transform.localScale);
        }

        private void ClearBush() => HideAllBerries();

        private void OnGenInitHandler()
        {
            ClearBush();
            RotateToRandomValue();
            _generator.generationProcess.OnStart += StartGenerationHandler;
            _generator.OnCollected += () => ClearBush();
        }

        private void RotateToRandomValue() => transform.Rotate(new Vector3(0f, Random.Range(0f, 360f), 0f));

        private void StartGenerationHandler(Generator generator) => StartCoroutine(Grow());

        private IEnumerator Grow()
        {
            while (IsGenerationProcessActive)
            {
                UpdateBerries(_generator.generationProcess.Value);
                yield return null;
            }
            UpdateBerries(1);
        }

        private bool IsGenerationProcessActive => _generator.generationProcess.Active;

        private void UpdateBerries(float val)
        {
            var zeroBerries = val < .25f;
            var oneBerry = val is >= .25f and < .5f;
            var twoBerries = val is >= .5f and < .75f;
            var threeBerries = val >= .75f;

            if (zeroBerries) ShowBerry(0);
            if (oneBerry) ShowBerry(1);
            if (twoBerries) ShowBerry(2);
        }

        private void ShowBerries(int value)
        {
            for (var i = 0; i < value; i++)
                ShowBerry(i);
        }

        private void ShowBerry(int i)
        {
            StartCoroutine(GrowBerry(berries[i]));
        }

        private IEnumerator GrowBerry(GameObject berry)
        {
            var elapsedTime = 0.0f;
            var index = berries.IndexOf(berry);

            void UpdateScale(float value) => berry.transform.localScale = berriesScales[index] * value;

            var time = _generator.model.GenerationSpeed.Value / 3;

            while (elapsedTime < time)
            {
                _value = elapsedTime / time;
                UpdateScale(_value);
                yield return null;
                elapsedTime += Time.deltaTime;
            }
            _value = 1.0f;
            UpdateScale(_value);
        }

        private void HideAllBerries()
        {
            foreach (var berry in berries)
                HideBerry(berry);
        }

        private static void HideBerry(GameObject berry)
        {
            berry.transform.localScale = Vector3.zero;
        }
    }
}