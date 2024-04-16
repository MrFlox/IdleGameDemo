using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Modules.Infrastructure.Services
{
    public class SceneLoader : ISceneLoader
    {
        public async void LoadScene(string sceneName, Action onComplete)
        {
            await LoadWithSceneManager(sceneName, onComplete);
        }
        private static async UniTask LoadWithSceneManager(string sceneName, Action onComplete)
        {
            await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
            onComplete?.Invoke();
        }
    }
}