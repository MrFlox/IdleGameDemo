using System.Collections.Generic;

namespace Modules.BehaviorTree
{
    public class Selector : BTNode
    {
        private readonly List<BTNode> _childNodes = new();

        public Selector(params BTNode[] nodes) => _childNodes.AddRange(nodes);

        public override TaskStatus Evaluate()
        {
            foreach (var childNode in _childNodes)
            {
                var result = childNode.Evaluate();

                if (result == TaskStatus.Success)
                {
                    return TaskStatus.Success;
                }

                if (result == TaskStatus.Failure)
                {
                    continue;
                }

                if (result == TaskStatus.Running)
                {
                    return TaskStatus.Running;
                }
            }

            return TaskStatus.Failure;
        }
    }
}