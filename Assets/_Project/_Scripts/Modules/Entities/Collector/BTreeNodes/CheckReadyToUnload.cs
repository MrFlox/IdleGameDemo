using Modules.BehaviorTree;

namespace Modules.Entities.Collector.BTreeNodes
{
    public sealed class CheckReadyToUnload : BTNode
    {
        private readonly Storage.Storage _fieldStorage;
        private readonly CollectorModel _model;
        public CheckReadyToUnload(Storage.Storage fieldStorage, CollectorModel model)
        {
            _fieldStorage = fieldStorage;
            _model = model;
        }

        public override TaskStatus Evaluate()
        {
            nodeState = isReadyToUnload() ? TaskStatus.Success : TaskStatus.Failure;
            return nodeState;
        }

        private bool isReadyToUnload() => _fieldStorage.NotFull() && _model.Copacity.Value > 0;
    }
}