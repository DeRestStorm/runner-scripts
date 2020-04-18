using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Controllers.Behaviours
{
    public class SocketBehaviour : MonoBehaviour
    {
        public List<ObjectSocket> Sockets;

        public bool Connect(Rigidbody rb)
        {
            var sockets = Sockets.Where(x => x.ObjectName == rb.name);
            foreach (var socket in sockets)
            {
                if (socket.Transform.GetComponentsInChildren<MonoBehaviour>().Length > 0)
                {
                    continue;
                }

                rb.MovePosition(socket.Transform.position);
                rb.MoveRotation(socket.Transform.rotation);

                return true;
            }

            return false;
        }

        [System.Serializable]
        public class ObjectSocket
        {
            [SerializeField] public Transform Transform;
            [SerializeField] public string ObjectName;
        }
    }
}