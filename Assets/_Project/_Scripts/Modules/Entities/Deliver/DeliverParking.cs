using Cysharp.Threading.Tasks;
using DG.Tweening;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Modules.Entities.Deliver
{
    [SelectionBase]
    public class DeliverParking : MonoBehaviour
    {
        public StorageConnection Connection;
        public Deliver Deliver;
        private IFactory _factory;

        [Inject] private void Construct(IFactory factory) => _factory = factory;

        private void Start()
        {
            transform.GetChild(0).TryGetComponent<MeshRenderer>(out var mesh);
            if (mesh != null)
                mesh.enabled = false;
        }

        [Button]
        public async UniTask<Deliver> AddDeliver()
        {
            if (Deliver == null)
            {
                Deliver = await _factory.CreateDeliver();
                Deliver.StorageConnection = Connection;
                Deliver.transform.rotation = transform.rotation;
                Deliver.transform.position = transform.position;
                Deliver.Init();
                Deliver.transform.localScale = new Vector3(0, 0, 0);
                Deliver.transform.DOScale(new Vector3(1, 1, 1), 1).SetEase(Ease.OutElastic);
            }
            return Deliver;
        }

        private void OnMouseDown() => AddDeliver().Forget();

        private void OnDrawGizmos()
        {
            DrawConnections();
        }

        private void DrawConnections()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Connection.StorageA.transform.position);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, Connection.StorageB.transform.position);
        }
    }
}