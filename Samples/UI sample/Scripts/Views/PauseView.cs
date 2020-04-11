using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class PauseView : BasePopup
    {
        [SerializeField] private Button _toMainMenu;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _resume;
        public event Action SettingsClicked = delegate { };
        public event Action HomeClicked = delegate { };
        public event Action ResumeClicked = delegate { };

        private void Start()
        {
            _settings.onClick.AddListener(() => SettingsClicked());
            _toMainMenu.onClick.AddListener(() => HomeClicked());
            _resume.onClick.AddListener(() => ResumeClicked());
        }
    }
}