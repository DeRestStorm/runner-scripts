using System.Collections;
using UnityEngine;
using Zenject;

namespace Controllers.Behaviours
{
    public class Platform : MonoBehaviour
    {
        [Inject] private PlayerMovement _pm;
        public float Time;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _pm.gameObject)
            {
                Invoke(nameof(Breack), Time);
            }
        }


        void Breack()
        {
            Debug.Log("Log");
            var rb = GetComponent<Rigidbody>(); 
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;

        }
    }
}