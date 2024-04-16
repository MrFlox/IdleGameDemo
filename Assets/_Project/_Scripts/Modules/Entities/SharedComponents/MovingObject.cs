using System;
using TriInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.Entities.SharedComponents
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovingObject : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Vector3 _target;
        private bool _active;
        private Action _onEndCallback;
        private float _speed = 1f;

        private void Awake() => _agent = GetComponent<NavMeshAgent>();

        public event Action OnStartMove;
        public event Action OnAtTheTarget;


        [Button]
        public void GoTo(Vector3 target, Action onEndCallback = null)
        {
            _onEndCallback = onEndCallback;
            _target = target;
            _agent.SetDestination(_target);
            ActivateMovement();
            OnStartMove?.Invoke();
        }

        [Button(ButtonSizes.Large, "To Transform")]
        public void GoTo(Transform trans)
        {
            GoTo(trans.position);
        }
        private void ActivateMovement() => _active = true;
        private void StopMovement() => _active = false;

        public bool isMoving() => _active;
        private void Update()
        {
            if (!_active) return;
            if (_agent.speed != _speed)
                _agent.speed = _speed;
            if (IsAtTheTarget)
            {
                StopMovement();
                OnAtTheTarget?.Invoke();
                _onEndCallback?.Invoke();
            }
        }

        private bool CheckDistance() => _agent.remainingDistance <= _agent.stoppingDistance;


        private bool IsAtTheTarget
        {
            get { return !_agent.pathPending && CheckDistance(); }
        }

        public void SetSpeed(float movingSpeedValue)
        {
            _speed = movingSpeedValue;

        }
        public NavMeshAgent GetAgent() => _agent;
        public bool AtTarget()
        {
            return IsAtTheTarget;
        }
    }
}