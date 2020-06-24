using System;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers.Behaviours
{
    public class NavPosition : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = FindObjectOfType<NavMeshAgent>();
        }

        public void Move()
        {
            _agent.SetDestination(transform.position);
        }
    }
}