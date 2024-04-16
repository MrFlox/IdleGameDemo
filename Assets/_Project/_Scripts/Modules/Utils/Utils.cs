using Modules.Core;

namespace Modules.Utils
{
    public sealed class Utils
    {
        public static ReactiveProperty<T> FromValue<T>(T value) => new(value);
    }
}