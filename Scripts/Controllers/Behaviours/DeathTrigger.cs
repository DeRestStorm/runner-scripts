using System;
using Scripts.Interfaces;
using Scripts.Models;
using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Controllers.Behaviours
{
    public class DeathTrigger : MonoBehaviour
    {
        [Inject] private PlayerMovement _playerMovement;
        [Inject] private ZenjectSceneLoader _sceneLoader;
        [Inject] private PauseController _pauseController;
        [Inject] private SignalBus _signalBus;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _playerMovement.gameObject)
            {
                _signalBus.Fire<DeathSignal>();
                Debug.Log("Death");

                // _sceneLoader.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single,
                //     (container) =>
                //     {
                //         container.BindInstance("default_level").WhenInjectedInto<IItemRepository<Item>>();
                //     });
            }
        }
    }
}