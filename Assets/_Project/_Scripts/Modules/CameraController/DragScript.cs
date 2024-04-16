using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.CameraController
{
    public class DragScript : MonoBehaviour
    {
        public event Action<Vector2> OnTap;

        public Camera cam;
        public float StartTime;
        public float MaxDurationForTap = 0.4f;
        public float MaxDistanceForTap = 40;

        private bool _isTouching;
        private Vector3 _delta;
        private Vector3 _startPosition = Vector3.zero;


        private void Start()
        {
            if (cam == null)
                cam = Camera.main;
        }

        private void OnClick(Vector2 position) => OnTap?.Invoke(position);

        private void Update()
        {
            var validTouches = Input.touches
                /*.Where(touch => !EventSystem.current.IsPointerOverGameObject(touch.fingerId))*/
                .ToArray();
            if (validTouches.Length != 1) return;

            var touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartTime = Time.time;
                    _startPosition = touch.position;
                    _isTouching = true;
                    break;
                case TouchPhase.Moved:
                    _delta = cam.ScreenToWorldPoint(touch.deltaPosition);
                    var pt1 = _delta;
                    var pt2 = cam.ScreenToWorldPoint(Vector2.zero);
                    transform.position -= pt1 - pt2;
                    break;
                case TouchPhase.Ended:
                    var b1 = Time.time - StartTime <= MaxDurationForTap;
                    var b2 = Vector2.Distance(touch.position, _startPosition) <= MaxDistanceForTap;
                    var b3 = _isTouching;

                    if (b1 && b2 && b3)
                        OnClick(touch.position);
                    _isTouching = false;
                    break;
            }
        }
    }
}