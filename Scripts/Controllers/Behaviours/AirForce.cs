using System;
using UnityEngine;

namespace Controllers.Behaviours
{
    public class AirForce : MonoBehaviour
    {
        public float Force;

        private void OnTriggerStay(Collider other)
        {
            var rb = other.GetComponent<Rigidbody>();
            if (rb == null)
                return;
            
            rb.AddForce(transform.forward * Force);
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.matrix = transform.localToWorldMatrix;

            var collider = GetComponent<BoxCollider>();
            if (collider != null)
            {
                Gizmos.DrawWireCube(collider.center, collider.size);
            }

            DrawArrow.ForGizmo(Vector3.zero, Vector3.forward, Color.cyan, .5f);
        }
    }
}