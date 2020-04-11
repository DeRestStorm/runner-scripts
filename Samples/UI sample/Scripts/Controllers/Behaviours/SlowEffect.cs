using System;
using UnityEngine;

namespace Controllers.Behaviours
{
    public class SlowEffect : MonoBehaviour, IEffect
    {
        [Range(-5f, 1f)]
        public float Strength;

        [Range(.5f, 20f)] [SerializeField] private float _time;

        public float Time
        {
            get => _time;
            set => _time = value;
        }

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
    }
}