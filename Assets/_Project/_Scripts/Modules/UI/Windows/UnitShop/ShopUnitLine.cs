using System;
using Modules.UI.Components;
using UnityEngine;

namespace Modules.UI.Windows.UnitShop
{
    [Serializable]
    public struct ShopUnitLine
    {
        public string Name;
        public Sprite Sprite;
        public int Price;
        public BuyButtonComponent.ButtonState ButtonState;
    }
}