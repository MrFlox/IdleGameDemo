using UnityEngine;

namespace Modules.Entities.Deliver
{
    [RequireComponent(typeof(Deliver))]
    public class DeliverView : MonoBehaviour
    {
        private Deliver _deliver;

        private void Awake() => _deliver = GetComponent<Deliver>();

        private void Start()
        {
            _deliver.OnChangeState += OnChangeState;
        }

        private void OnChangeState(Deliver.States state)
        {
        }
    }
}