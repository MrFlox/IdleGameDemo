using UnityEngine;

namespace Modules.Signals
{
    public record FlyingTextSignal(int Amount, Vector3 Position)
    {
        public int Amount { get; } = Amount;
        public Vector3 Position { get; } = Position;
    }
}