using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Components
{
    public class TabDescriptionComponent : MonoBehaviour
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _image;

        public void Init(string header, string description, Sprite image = null)
        {
            _header.text = header;
            _description.text = description;
            if (image != null)
            {
                _image.sprite = image;
            }
        }

        public void Init(TabInfo tabInfo) => Init(tabInfo.Header, tabInfo.Description, tabInfo.Image);
    }
}