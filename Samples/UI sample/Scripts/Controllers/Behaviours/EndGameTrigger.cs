using System;
using Scripts.Interfaces;
using Scripts.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Controllers.Behaviours
{
    public class EndGameTrigger : MonoBehaviour
    {
        [Inject] private PlayerMovement _playerMovement;
        [Inject] private ZenjectSceneLoader _sceneLoader;
        [Inject] private PauseController _pauseController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _playerMovement.gameObject)
            {
                _pauseController.Pause();
                // _sceneLoader.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single,
                //     (container) =>
                //     {
                //         container.BindInstance("default_level").WhenInjectedInto<IItemRepository<Item>>();
                //     });
            }
        }
    }
}