using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Controllers.Behaviours
{
    public class DeathTrigger : MonoBehaviour
    {
        [Inject] private PlayerMovement _playerMovement;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _playerMovement.gameObject)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}