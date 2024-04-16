namespace Modules.BehaviorTree
{
    public abstract class BTNode
    {
        protected TaskStatus nodeState;
        public abstract TaskStatus Evaluate();
    }
}