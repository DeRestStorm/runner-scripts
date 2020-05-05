using Commands;
using Controllers;
using Signals;
using States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Views;
using Zenject;

namespace Runtime.Scripts.States
{
    public class DeathState : BaseState
    {
        [Inject] private DeathView _pauseView;
        [Inject] private SignalBus _signalBus;
        [Inject] private PauseController _pauseController;
        
        [Inject] private StartRunnerSceneCommand _startRunnerSceneCommand;

        public override void Load()
        {
            base.Load();
            _pauseView.RestartClicked += OnRestartClicked;
            _pauseView.HomeClicked += OnHomeClicked;
            _pauseView.Show();
            _pauseController.Pause();
        }

        public override void Unload()
        {
            base.Unload();
            _pauseView.RestartClicked -= OnRestartClicked;
            _pauseView.HomeClicked -= OnHomeClicked;
            _pauseView.Hide();
            _pauseController.UnPause();
        }

        private void OnRestartClicked()
        {
            _signalBus.Fire<HardwareBackPressSignal>();
             _startRunnerSceneCommand.Exequte();
        }
        
        private void OnHomeClicked()
        {
            _signalBus.Fire<HardwareBackPressSignal>();
            SceneManager.LoadScene(0);
        }
    }
}