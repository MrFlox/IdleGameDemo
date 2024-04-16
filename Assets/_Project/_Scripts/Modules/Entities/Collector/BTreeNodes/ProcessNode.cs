using Modules.BehaviorTree;
using Modules.Core;

namespace Modules.Entities.Collector.BTreeNodes
{
    public class ProcessNode : BTNode
    {
        private readonly IdleProcess<Collector> _activeProcess;

        public ProcessNode(IdleProcess<Collector> activeProcess) =>
            _activeProcess = activeProcess;

        public override TaskStatus Evaluate()
        {
            if (_activeProcess.Finished)
            {
                OnFinishProcess();
                _activeProcess.Clear();
                nodeState = TaskStatus.Success;
                return nodeState;
            }
            if (!_activeProcess.Active && !_activeProcess.Finished) StartProcess();
            if (_activeProcess.Active && !_activeProcess.Finished) return TaskStatus.Running;
            return TaskStatus.Running;
        }

        protected virtual void OnFinishProcess()
        {
        }

        protected virtual void StartProcess() => _activeProcess.Start();
    }
}