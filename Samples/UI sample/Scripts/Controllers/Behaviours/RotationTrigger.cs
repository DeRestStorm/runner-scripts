using System;
using UnityEngine;

namespace Controllers.Behaviours
{
    public class RotationTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var rotation = other.transform.rotation;
            rotation.y = transform.rotation.y;
            other.transform.rotation = rotation;
        }
    }
}