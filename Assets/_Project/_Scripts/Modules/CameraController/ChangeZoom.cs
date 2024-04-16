using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.CameraController
{
    public class ChangeZoom : MonoBehaviour
    {
        private bool isTouching;
        public bool zoomEffect = false;
        [FormerlySerializedAs("camera")] public Cinemachine.CinemachineVirtualCamera c;
        [SerializeField] private float zoomAnimationDuration = .3f;
        private Vector3 lastCamPositon;
        private float lastCamZoom;
        public void focusOnObject(Vector3 pos, bool useZoom = false)
        {
            lastCamPositon = gameObject.transform.position;
            lastCamZoom = c.m_Lens.OrthographicSize;

            StartCoroutine(Focus(pos - Vector3.up * 1.5f, useZoom ? 4.0f : lastCamZoom));
        }

        private IEnumerator Focus(Vector3 pos, float zoom)
        {
            var startTime = Time.time;
            while (Time.time - startTime < zoomAnimationDuration)
            {
                var t = (Time.time - startTime) / zoomAnimationDuration;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, pos, t);
                c.m_Lens.OrthographicSize = Mathf.Lerp(c.m_Lens.OrthographicSize, zoom, t);
                yield return null;
            }

            gameObject.transform.position = pos;
            c.m_Lens.OrthographicSize = zoom;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var newZoom = c.m_Lens.OrthographicSize + 3.0f;
                LeanTween.value(gameObject, OnUpdate, c.m_Lens.OrthographicSize, newZoom, 1.0f);
                // c.m_Lens.OrthographicSize += 1.0f;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                var newZoom = c.m_Lens.OrthographicSize - 3.0f;
                LeanTween.value(gameObject, OnUpdate, c.m_Lens.OrthographicSize, newZoom, 1.0f);
                // c.m_Lens.OrthographicSize -= 1.0f;
            }

            var touchCount = Input.touches.Length;

            if (touchCount == 2)
            {
                var touch0 = Input.touches[0];
                var touch1 = Input.touches[1];

                if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended) return;

                isTouching = true;

                var previousDistance = Vector2.Distance(touch0.position - touch0.deltaPosition,
                    touch1.position - touch1.deltaPosition);

                var currentDistance = Vector2.Distance(touch0.position, touch1.position);

                if (previousDistance != currentDistance)
                {
                    OnPinch(previousDistance,
                        currentDistance);
                }
            }

            if (touchCount == 0)
            {
                if (c.m_Lens.OrthographicSize == 9.0f)
                {
                    StartZoomEffect(c.m_Lens.OrthographicSize - c.m_Lens.OrthographicSize * 0.05f);
                }

                if (c.m_Lens.OrthographicSize == 5.0f)
                {
                    StartZoomEffect(c.m_Lens.OrthographicSize + c.m_Lens.OrthographicSize * 0.05f);
                }
            }
        }

        private void StartZoomEffect(float zoomOrtSize)
        {
            if (zoomEffect) return;
            zoomEffect = true;
            LeanTween.value(gameObject, OnUpdate, c.m_Lens.OrthographicSize, zoomOrtSize, 0.25f)
                .setOnComplete(() => zoomEffect = false);
        }

        private void OnPinch(float oldDistance, float newDistance)
        {
            var cam = Camera.main;
            var zoomKoef = oldDistance / newDistance;
            // c.m_Lens.OrthographicSize = Mathf.Max(0.1f, c.m_Lens.OrthographicSize * zoomKoef);
            c.m_Lens.OrthographicSize = Mathf.Clamp(c.m_Lens.OrthographicSize * zoomKoef, 5.0f, 9.0f);
        }

        private void OnUpdate(float value) => c.m_Lens.OrthographicSize = value;
    }
}