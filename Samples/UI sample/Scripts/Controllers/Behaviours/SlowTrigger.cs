using System;
using UnityEngine;

namespace Controllers.Behaviours
{
    public class SlowTrigger : MonoBehaviour, IEffect
    {
        [Range(-5f, 1f)] public float Strength;
        [SerializeField] [Range(.5f, 20f)] private float _time;
        public float Time {get => _time; set => _time = value; }
        [HideInInspector] public DateTime BeginTime { get; set; }

        public void Begin(GameObject go)
        {
            
            Debug.Log("Begin");
            var playerMovement = go.GetComponent<PlayerMovement>();
            playerMovement.Modifer -= Strength;
            
            Debug.Log(playerMovement.Modifer);
        }

        public void End(GameObject go)
        {
            var playerMovement = go.GetComponent<PlayerMovement>();
            playerMovement.Modifer += Strength;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Add");
            
            var effects = other.GetComponent<CharacterEffects>();
            if (effects == null)
                return;

            effects.Add(this);
        }
        
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.matrix = transform.localToWorldMatrix;

            var collider = GetComponent<BoxCollider>();
            if (collider != null)
            {
                Gizmos.DrawWireCube(collider.center, collider.size);
            }
        }
    }
}