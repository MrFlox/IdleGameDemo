using UnityEngine;

namespace Modules.Entities.Converter
{
    [RequireComponent(typeof(Converter))]
    public class ConverterView : MonoBehaviour
    {
        private const string MachineIdle = "Idle";
        private const string NewBarrelAnimation = "New_Barrel";
        private const string WorderActive = "Working";
        private const string WorkmanIdle = "AS_Workman_Idle";

        [SerializeField] private Animator[] WorkersAnimators;
        [SerializeField] private Animator MachineAnimator;

        private Converter _converter;

        private void Awake()
        {
            _converter = GetComponent<Converter>();
            _converter.OnChangeState += OnChangeState;
        }
        private void Start()
        {
            DeactivateWorkers();
            DeactivateMachine();
        }
        private void OnChangeState(Converter.States state)
        {
            switch (state)
            {
                case Converter.States.Active:
                    ActivateWorkers();
                    ActivateMachine();
                    break;
                case Converter.States.Inactive:
                    DeactivateWorkers();
                    DeactivateMachine();
                    break;
            }
        }

        private void DeactivateMachine() => MachineAnimator.Play(MachineIdle);

        private void ActivateMachine() => MachineAnimator.Play(NewBarrelAnimation);

        private void ActivateWorkers()
        {
            foreach (var worker in WorkersAnimators)
            {
                worker.Play(WorderActive);
            }
        }

        private void DeactivateWorkers()
        {
            foreach (var worker in WorkersAnimators)
            {
                worker.Play(WorkmanIdle);
            }
        }
    }
}