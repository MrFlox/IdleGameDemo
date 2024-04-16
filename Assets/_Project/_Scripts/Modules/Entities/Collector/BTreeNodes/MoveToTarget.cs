using System;
using Modules.BehaviorTree;
using Modules.Entities.SharedComponents;
using UnityEngine;

namespace Modules.Entities.Collector.BTreeNodes
{
    public sealed class MoveToTarget : BTNode
    {
        public Func<Transform> ActiveGeneratorTransform { get; }
        private readonly MovingObject _moving;
        private readonly Func<Vector3> _activeGeneratorPosition;
        private Vector3 _target;
        private readonly IGameEntity<Collector.States> _collector;
        private bool _isMoveStarted;

        public MoveToTarget(MovingObject moving, Func<Transform> activeGeneratorTransform, Collector collector)
        {
            ActiveGeneratorTransform = activeGeneratorTransform;
            _moving = moving;
            _collector = collector;
        }
        public MoveToTarget(MovingObject moving, Func<Vector3> activeGeneratorPosition, Collector collector)
        {
            _moving = moving;
            _activeGeneratorPosition = activeGeneratorPosition;
            _collector = collector;
        }

        public override TaskStatus Evaluate()
        {
            _target = GetTarget();
            _moving.GoTo(_target);
            _collector.SetState(Collector.States.Walk);
            if (_moving.AtTarget()) return TaskStatus.Success;
            if (_moving.isMoving()) return TaskStatus.Running;

            return TaskStatus.Running;
        }

        private Vector3 GetTarget()
        {
            if (ActiveGeneratorTransform != null)
                return ActiveGeneratorTransform.Invoke().position;
            if (_activeGeneratorPosition != null)
                return _activeGeneratorPosition.Invoke();
            return Vector3.zero;
        }
    }
}