using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveToClickPoint : MonoBehaviour
    {
        private NavMeshAgent _agent;

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    _agent.destination = hit.point;
                }
            }
        }
    }
}