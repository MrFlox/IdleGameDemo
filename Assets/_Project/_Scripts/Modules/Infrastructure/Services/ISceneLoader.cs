using System;

namespace Modules.Infrastructure.Services
{
    public interface ISceneLoader
    {
        void LoadScene(string sceneName, Action onComplete);
    }
}