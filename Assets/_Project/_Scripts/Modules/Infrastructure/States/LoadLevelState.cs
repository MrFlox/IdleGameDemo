using Cysharp.Threading.Tasks;
using MessagePipe;
using Modules.Signals;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Modules.Infrastructure.States
{
    public class LoadLevelState : IGameState
    {
        private const string LevelAddress = "Level";
        private readonly IStateManager _stateMachine;
        private readonly IPublisher<LoadingSignal, float> _loadingSignal;

        public LoadLevelState(IStateManager stateMachine, IPublisher<LoadingSignal, float> loadingSignal)
        {
            _stateMachine = stateMachine;
            _loadingSignal = loadingSignal;
        }

        public void EnterState() => LoadLevelAsync().Forget();

        private async UniTask LoadLevelAsync()
        {
            var handle = Addressables.LoadSceneAsync(LevelAddress);
            while (!handle.IsDone)
            {
                _loadingSignal.Publish(LoadingSignal.Progress, handle.PercentComplete);
                await UniTask.Yield(); 
            }

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                OnComplete();
            }
            else
            {
                Debug.LogError("Failed to load the asset.");
            }
        }

        private void OnComplete() => _stateMachine.SetState(StateMachine.States.Init);
    }
}