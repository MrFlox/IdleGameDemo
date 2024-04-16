using System;
using Constants;
using Modules.UI.Windows.UnitShop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Components
{
    public class BuyUnitComponent : MonoBehaviour
    {
        [SerializeField] private BuyButtonComponent _buyButton;
        [SerializeField] private Image _unitImage;
        [SerializeField] private TMP_Text _unitNameText;

        public void Init(ShopUnitLine unitInfo, Action addUnit)
        {
            _unitNameText.text = unitInfo.Name;
            _unitImage.sprite = unitInfo.Sprite;
            _unitImage.color = Color.white;
            _buyButton.Init("Buy", unitInfo.Price, unitInfo.ButtonState, addUnit);

        }
    }
}