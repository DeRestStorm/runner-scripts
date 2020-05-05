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
    public class MenuState : BaseState
    {
        [Inject] private PauseView _pauseView;
        [Inject] private SignalBus _signalBus;
        [Inject] private PauseController _pauseController;
        
        [Inject] private StartRunnerSceneCommand _startRunnerSceneCommand;

        public override void Load()
        {
            base.Load();
            _pauseView.SettingsClicked += OnSettingsClicked;
            _pauseView.HomeClicked += OnHomeClicked;
            _pauseView.ResumeClicked += OnResumeClicked;
            _pauseView.Show();
            _pauseController.Pause();
        }

        public override void Unload()
        {
            base.Unload();
            _pauseView.SettingsClicked -= OnSettingsClicked;
            _pauseView.HomeClicked -= OnHomeClicked;
            _pauseView.ResumeClicked -= OnResumeClicked;
            _pauseView.Hide();
            _pauseController.UnPause();
        }

        private void OnSettingsClicked()
        {
            _signalBus.Fire<HardwareBackPressSignal>();
            Debug.Log("Нажали настройки");
             _startRunnerSceneCommand.Exequte();
        }
        
        private void OnHomeClicked()
        {
            _signalBus.Fire<HardwareBackPressSignal>();
            SceneManager.LoadScene(0);
        }

        private void OnResumeClicked()
        {
            // _startRunnerSceneCommand.Exequte();
            _signalBus.Fire<HardwareBackPressSignal>();
            // Unload();
            // _signalBus.
            
        }
    }
}