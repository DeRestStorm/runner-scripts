using System;
using Scripts.Interfaces;
using Scripts.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

namespace Controllers.Behaviours
{
    public class EndGameTrigger : MonoBehaviour
    {
        [Inject] private PlayerMovement _playerMovement;
        [Inject] private ZenjectSceneLoader _sceneLoader;
        [Inject] private PauseController _pauseController;
        public UnityEvent OnEnd;
        public bool PauseOnEnd;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _playerMovement.gameObject)
            {
                if (PauseOnEnd)
                    _pauseController.Pause();
                OnEnd?.Invoke();
            }
        }
    }
}