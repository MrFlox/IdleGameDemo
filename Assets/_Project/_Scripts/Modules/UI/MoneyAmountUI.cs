using Modules.Core;
using Modules.ResourceSystem;
using TMPro;
using UnityEngine;
using VContainer;

namespace Modules.UI
{
    public class MoneyAmountUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [Inject] private IResourcesService _resourcesService;
        private ReactiveProperty<int> _targetValue;

        private void Awake()
        {
            _targetValue = _resourcesService.Get(Account.Type.Money);
            _targetValue.Subscribe(OnChange);
        }

        private void OnChange(int newAmount = 0) =>
            text.text = _targetValue.Value.ToString();

        private void Start() => OnChange();
    }
}