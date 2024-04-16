using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TriInspector;

namespace Modules.UI.Components
{
    public class BuyButtonComponent : MonoBehaviour
    {
        [SerializeField] private TMP_Text _buttonLabel;
        [SerializeField] private TMP_Text _amoutText;
        [SerializeField] private Image _check;
        [SerializeField] private Image _lock;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Button _button;

        private ButtonState _currentState = ButtonState.None;

        public void Init(string buttonLabel, int amout, ButtonState unitInfoButtonState, Action callback)
        {
            _button.onClick.AddListener(() => callback());
            _buttonLabel.text = buttonLabel;
            _amoutText.text = amout.ToString();
            SetState(unitInfoButtonState);
        }

        private void OnDisable() => _button.onClick.RemoveAllListeners();

        public enum ButtonState
        {
            None,
            Enabled,
            Disabled,
            Locked,
            Bought
        }

        [Button]
        public void SetState(ButtonState state)
        {
            if (_currentState == state) return;
            _currentState = state;
            switch (_currentState)
            {
                case ButtonState.Enabled:
                    EnableButton();
                    break;
                case ButtonState.Disabled:
                    DisableButton();
                    break;
                case ButtonState.Locked:
                    LockedMode();
                    break;
                case ButtonState.Bought:
                    BoughtState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void BoughtState()
        {
            _button.interactable = false;
            _check.enabled = true;
            _lock.enabled = false;
            _buttonImage.enabled = false;
            _amoutText.enabled = false;
            _buttonLabel.enabled = false;
        }

        private void DisableButton()
        {
            _button.interactable = false;
            _check.enabled = false;
            _lock.enabled = false;
            _buttonImage.enabled = true;
            _amoutText.enabled = true;
            _buttonLabel.enabled = true;
        }

        private void EnableButton()
        {
            _button.interactable = true;
            _check.enabled = false;
            _lock.enabled = false;
            _buttonImage.enabled = true;
            _amoutText.enabled = true;
            _buttonLabel.enabled = true;
        }

        private void LockedMode()
        {
            _button.interactable = false;
            _check.enabled = false;
            _lock.enabled = true;
            _buttonImage.enabled = false;
            _amoutText.enabled = false;
            _buttonLabel.enabled = false;
        }
    }
}