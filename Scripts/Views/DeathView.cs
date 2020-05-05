using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class DeathView : BasePopup
    {
        [SerializeField] private Button _toMainMenu;
        [SerializeField] private Button _restart;
        public event Action RestartClicked = delegate { };
        public event Action HomeClicked = delegate { };

        private void Start()
        {
            _restart.onClick.AddListener(() => RestartClicked());
            _toMainMenu.onClick.AddListener(() => HomeClicked());
        }
    }
}