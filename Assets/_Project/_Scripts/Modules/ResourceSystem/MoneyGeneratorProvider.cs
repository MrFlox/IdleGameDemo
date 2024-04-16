using System;
using MessagePipe;
using Modules.Entities;
using Modules.Signals;
using UnityEngine;
using VContainer;

namespace Modules.ResourceSystem
{
    [RequireComponent(typeof(IMoneyActionObject))]
    public class MoneyGeneratorProvider : MonoBehaviour
    {
        public event Action OnMoneyGenerated;
        [SerializeField] public int MoneyPerTick;

        private IResourcesService _resourcesService;
        private IMoneyActionObject _collector;
        private IPublisher<FlyingTextSignal> _publisher;

        [Inject] private void Construct(IResourcesService resourcesService, IPublisher<FlyingTextSignal> publisher)
        {
            _publisher = publisher;
            _resourcesService = resourcesService;
        }

        private void Awake()
        {
            _collector = GetComponent<IMoneyActionObject>();
            _collector.OnMoneyAction += MoneyAction;
        }

        private void MoneyAction() => Generate();

        private void OnDestroy() => _collector.OnMoneyAction -= MoneyAction;

        private void Generate()
        {
            _resourcesService.Add(Account.Type.Money, MoneyPerTick);
            CreateFlyingText();
            OnMoneyGenerated?.Invoke();
        }

        private void CreateFlyingText() =>
            _publisher.Publish(new FlyingTextSignal(MoneyPerTick, transform.position));
    }
}