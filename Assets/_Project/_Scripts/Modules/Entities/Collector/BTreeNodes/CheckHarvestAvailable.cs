using Modules.BehaviorTree;

namespace Modules.Entities.Collector.BTreeNodes
{
    public sealed class CheckHarvestAvailable : BTNode
    {
        private Generator.Generator _generator;

        public CheckHarvestAvailable(Generator.Generator generator) => _generator = generator;

        public override TaskStatus Evaluate()
        {
            nodeState = _generator.IsReadyForHarvest() ? TaskStatus.Success : TaskStatus.Failure;
            return nodeState;
        }
    }
}