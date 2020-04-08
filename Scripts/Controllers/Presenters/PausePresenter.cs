using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Views;
using Zenject;

namespace Controllers.Presenters
{
    public class PausePresenter
    {
        private PauseView _pauseView;

        [Inject]
        public PausePresenter(PauseView pauseView)
        {
            // pauseView.ToMainMenu.onClick.AddListener(() => { SceneManager.LoadScene(0); });
        }
    }
}