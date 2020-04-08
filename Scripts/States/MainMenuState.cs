using UnityEngine;
using UnityEngine.SceneManagement;
using Views;
using Zenject;

namespace States
{
    public class MainMenuState : IState
    {
        [Inject] private MainMenuView _mainMenuView;

        public void Load()
        {
            _mainMenuView.StartClicked += OnStartClicked;
            _mainMenuView.Show();
        }

        public void Unload()
        {
            _mainMenuView.StartClicked -= OnStartClicked;
            _mainMenuView.Hide();
        }

        private void OnStartClicked()
        {
            SceneManager.LoadScene(1);
        }
    }
}