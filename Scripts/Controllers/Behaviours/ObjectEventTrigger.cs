using UnityEngine;
using UnityEngine.Events;

namespace Controllers.Behaviours
{
    public class ObjectEventTrigger : MonoBehaviour
    {
        public string ObjectName;
        [Space]
        public UnityEvent OnEnter = new UnityEvent();
        public UnityEvent OnExit = new UnityEvent();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name != ObjectName)
                return;
                
            OnEnter.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name != ObjectName)
                return;
            
            OnExit.Invoke();
        }
    }
}