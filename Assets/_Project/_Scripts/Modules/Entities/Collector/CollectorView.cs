using System.Collections.Generic;
using Modules.Entities.SharedComponents;
using Unity.Collections;
using UnityEngine;

namespace Modules.Entities.Collector
{
    [RequireComponent(typeof(Collector))] [RequireComponent(typeof(MovingObject))]
    public class CollectorView : MonoBehaviour
    {
        private readonly static Dictionary<VisualStates, int> VisStates;
        private readonly static int Idle = Animator.StringToHash("AS_Workman_Hat_Idle");
        private readonly static int Walk = Animator.StringToHash("AS_Workman_Hat_Walking");
        private readonly static int Work = Animator.StringToHash("AS_Workman_Hat_Working");

        [ReadOnly] [SerializeField] private VisualStates CurrentVisualState = VisualStates.None;
        [SerializeField] private GameObject Model;
        [SerializeField] private Animator Animator;
        private Collector _collector;
        static CollectorView()
        {
            VisStates = new Dictionary<VisualStates, int>
            {
                [VisualStates.Idle] = Idle,
                [VisualStates.Walk] = Walk,
                [VisualStates.Work] = Work
            };
        }
        public enum VisualStates
        {
            None,
            Idle,
            Walk,
            Work
        }

        public void SetVisualState(VisualStates newVisualState)
        {
            if (CurrentVisualState == newVisualState) return;
            CurrentVisualState = newVisualState;
            Animator.Play(VisStates[CurrentVisualState]);
        }
        private void Awake()
        {
            _collector = GetComponent<Collector>();
            _collector.OnChangeState += OnChangeCollectorState;
        }
        private void OnChangeCollectorState(Collector.States state)
        {
            switch (state)
            {
                case Collector.States.Walk:
                    SetVisualState(VisualStates.Walk);
                    break;
                case Collector.States.Work:
                    SetVisualState(VisualStates.Work);
                    break;
                case Collector.States.Idle:
                    SetVisualState(VisualStates.Idle);
                    break;
                default:
                    break;
            }
        }
    }
}