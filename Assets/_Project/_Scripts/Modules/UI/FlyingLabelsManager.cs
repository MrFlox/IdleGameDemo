using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MessagePipe;
using Modules.Entities;
using Modules.Entities.Services;
using Modules.Signals;
using VContainer.Unity;

namespace Modules.UI
{
    public class FlyingLabelsManager : IStartable
    {
        private readonly ISubscriber<FlyingTextSignal> _subscriber;
        private readonly AssetLoaderService _assetLoaderService;
        private const string LabelID = "UI/Components/FlyingText.prefab";
        private IAbstractFactory<FlyingText> _labelFactory;

        public FlyingLabelsManager(
            ISubscriber<FlyingTextSignal> subscriber,
            AssetLoaderService assetLoaderService,
            IAbstractFactory<FlyingText> labelFactory
        )
        {
            _subscriber = subscriber;
            _assetLoaderService = assetLoaderService;
            _labelFactory = labelFactory;
        }

        public async void Start()
        {
            await InitFactory();
            _subscriber.Subscribe(Handle);
        }

        private async UniTask InitFactory()
        {
            var flyingTextPrefab = await _assetLoaderService.LoadAssetReference(LabelID);
            var flyLabel = flyingTextPrefab.GetComponent<FlyingText>();
            _labelFactory.Init(new List<FlyingText> { flyLabel });
        }

        private void Handle(FlyingTextSignal signal) => HandleFlyingText(signal);

        private void HandleFlyingText(FlyingTextSignal signal)
        {
            var flyingText = _labelFactory.Create();
            flyingText.SetText(signal.Amount);
            flyingText.transform.position = signal.Position;
            flyingText.OnComplete += HandleFlyingTextOnComplete;
        }

        private void HandleFlyingTextOnComplete(FlyingText flyingText)
        {
            flyingText.OnComplete -= HandleFlyingTextOnComplete;
            _labelFactory.Release(flyingText);
        }
    }
}