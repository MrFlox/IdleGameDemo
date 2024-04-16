using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Components
{
    public class TabButton : MonoBehaviour
    {
        public Button Button;
        [SerializeField] private Image _buttonImage;

        private Color _imageColor;

        public void ActivateTab(bool state)
        {
            _imageColor = _buttonImage.color;
            _imageColor.a = state ? 1 : 0.5f;
            _buttonImage.color = _imageColor;
        }
    }
}