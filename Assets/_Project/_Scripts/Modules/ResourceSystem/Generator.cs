using System;

namespace Modules.ResourceSystem
{
    public class Generator
    {
        public event Action OnGenerate;
        public int ResourcePerCirle;
        private readonly Account _account;

        public Generator(int resourcePerCirle, Account account)
        {
            ResourcePerCirle = resourcePerCirle;
            _account = account;
        }

        public void Generate()
        {
            _account.Add(ResourcePerCirle);
            OnGenerate?.Invoke();
        }
    }
}