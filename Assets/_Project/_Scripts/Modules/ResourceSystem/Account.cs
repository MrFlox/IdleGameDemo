using Modules.Core;

namespace Modules.ResourceSystem
{
    public class Account
    {
        public Type type;
        public enum Type
        {
            Money,
            Wine
        }
        public Account(Type type, int amount = 0)
        {
            this.type = type;
            Amount = new ReactiveProperty<int>(amount);
        }
        public ReactiveProperty<int> Amount { get; private set; }

        public void Add(int value = 1) => Amount.Value += value;

        public void Spend(int value = 1) => Amount.Value -= value;

    }
}