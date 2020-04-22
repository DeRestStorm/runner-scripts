using System;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Views
{
    public class MainMenuView : BasePopup
    {
        [SerializeField] private Button _startButton;
        public event Action StartClicked = delegate { };
        [Inject] private SignalBus _signalBus { get;}

        private void Start()
        {
            _signalBus.Fire<StartSceneSignal>();
            _startButton.onClick.AddListener(() => StartClicked());
        }
    }
}