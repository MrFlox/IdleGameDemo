using UnityEngine;

namespace Modules.Entities.SharedComponents
{
    public class ShadowScript : MonoBehaviour
    {
        public Transform target;

        private Vector3 _tempPos = Vector3.zero;
        private float opacity = 1f;
        private Color _tempColor;
        private SpriteRenderer _sprite;
        private float _initOpacity;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _initOpacity = _sprite.color.a;
        }
        private void Update()
        {
            if (target == null) return;
            UpdatePosition();
            UpdateOpacity();
        }
        private void UpdateOpacity()
        {
            opacity = 1 - target.transform.position.y / 7f;
            _tempColor = _sprite.color;
            _tempColor.a = opacity * _initOpacity;
            _sprite.color = _tempColor;
        }
        private void UpdatePosition()
        {
            _tempPos = target.transform.position;
            _tempPos.y = 0.2f;
            transform.position = _tempPos;
        }
    }
}