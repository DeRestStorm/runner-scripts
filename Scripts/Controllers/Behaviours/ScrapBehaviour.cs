using System;
using UnityEngine;
using Zenject;

namespace Controllers.Behaviours
{
    public class ScrapBehaviour : MonoBehaviour
    {
        [Inject] private Vector3 _spawnPosition;

        private void Start()
        {
            transform.position = _spawnPosition;
        }
    }
}