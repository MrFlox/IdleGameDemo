using Modules.BehaviorTree;

namespace Modules.Entities.Collector.BTreeNodes
{
    public sealed class Wait : BTNode
    {
        private readonly Collector _collector;

        public Wait(Collector collector) => _collector = collector;

        public override TaskStatus Evaluate()
        {
            _collector.SetState(Collector.States.Idle);
            return TaskStatus.Running;
        }
    }
}