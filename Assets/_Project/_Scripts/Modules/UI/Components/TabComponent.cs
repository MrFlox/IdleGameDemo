using System.Collections.Generic;
using UnityEngine;

namespace Modules.UI.Components
{

    public class TabComponent : MonoBehaviour
    {
        [SerializeField] private List<TabButton> _tabButtons;
        [SerializeField] private List<RectTransform> _tabs;
        [SerializeField] private TabDescriptionComponent _tabDescription;
        [SerializeField] private List<TabInfo> _tabInfos;

        private void OnEnable()
        {
            if (!IsValidateButtonsAndTabs())
            {
                Debug.LogError("Wrong Tabs or Buttons");
                return;
            }

            InitButtons();
            ActivateTab(0);
        }

        private void InitButtons()
        {
            for (var i = 0; i < _tabButtons.Count; i++)
            {
                var tabButton = _tabButtons[i];
                var currentTab = i;
                tabButton.Button.onClick.AddListener(() => ActivateTab(currentTab));
            }
        }

        private void ActivateTab(int tabIndex)
        {
            for (var i = 0; i < _tabs.Count; i++)
            {
                var state = i == tabIndex;
                _tabs[i].gameObject.SetActive(state);
                _tabButtons[i].ActivateTab(state);
            }

            _tabDescription.Init(_tabInfos[tabIndex]);
        }

        private void RemoveListeners()
        {
            foreach (var tabButton in _tabButtons)
                tabButton.Button.onClick.RemoveAllListeners();
        }

        private void OnDisable() => RemoveListeners();

        private bool IsValidateButtonsAndTabs() =>
            _tabButtons.Count == _tabs.Count && _tabButtons.Count == _tabInfos.Count;
    }
}