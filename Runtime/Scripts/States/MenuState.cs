using Commands;
using UnityEngine;
using UnityEngine.SceneManagement;
using Views;
using Zenject;

namespace States
{
    public class MenuState : BaseState
    {
        [Inject] private PauseView _pauseView;
        [Inject] private SignalBus _signalBus;
        
        [Inject] private StartRunnerSceneCommand _startRunnerSceneCommand;

        public override void Load()
        {
            base.Load();
            _pauseView.SettingsClicked += OnSettingsClicked;
            _pauseView.HomeClicked += OnHomeClicked;
            _pauseView.ResumeClicked += OnResumeClicked;
            _pauseView.Show();
        }

        public override void Unload()
        {
            base.Unload();
            _pauseView.SettingsClicked -= OnSettingsClicked;
            _pauseView.HomeClicked -= OnHomeClicked;
            _pauseView.ResumeClicked -= OnResumeClicked;
            _pauseView.Hide();
        }

        private void OnSettingsClicked()
        {
            Debug.Log("Нажали настройки");
        }
        
        private void OnHomeClicked()
        {
            SceneManager.LoadScene(0);
        }    private void OnResumeClicked()
        {
            _startRunnerSceneCommand.Exequte();
            Unload();
            // _signalBus.
        }
    }
}