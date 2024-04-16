namespace Modules.BehaviorTree
{
    public class Sequence : BTNode
    {
        private readonly BTNode[] _nodes;
        private int _currentIndex = 0;

        public Sequence(params BTNode[] nodes) => _nodes = nodes;

        public override TaskStatus Evaluate()
        {
            for (var i = _currentIndex; i < _nodes.Length; i++)
            {
                var nodeState = _nodes[i].Evaluate();

                if (nodeState == TaskStatus.Success)
                {
                    _currentIndex++;
                    if (_currentIndex == _nodes.Length)
                    {
                        _currentIndex = 0; // Сбрасываем индекс для следующего выполнения
                        return TaskStatus.Success;
                    }
                }
                else
                {
                    return nodeState;
                }
            }
            _currentIndex = 0;
            return TaskStatus.Success;
        }
    }
}