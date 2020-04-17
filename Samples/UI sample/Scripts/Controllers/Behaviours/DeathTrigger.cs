using System;
using Scripts.Interfaces;
using Scripts.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Controllers.Behaviours
{
    public class DeathTrigger : MonoBehaviour
    {
        [Inject] private PlayerMovement _playerMovement;
        [Inject] private ZenjectSceneLoader _sceneLoader;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _playerMovement.gameObject)
            {
                _sceneLoader.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single,
                    (container) =>
                    {
                        container.BindInstance("default_level").WhenInjectedInto<IItemRepository<Item>>();
                    });
            }
        }
    }
}