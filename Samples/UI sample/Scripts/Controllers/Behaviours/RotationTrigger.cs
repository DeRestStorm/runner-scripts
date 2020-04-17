using System;
using UnityEngine;
using Zenject;

namespace Controllers.Behaviours
{
    public class RotationTrigger : MonoBehaviour
    {
        [Inject] private PlayerMovement _playerMovement;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.matrix = transform.localToWorldMatrix;

            var collider = GetComponent<BoxCollider>();
            if (collider != null)
            {
                Gizmos.DrawWireCube(collider.center, collider.size);
            }

            DrawArrow.ForGizmo(Vector3.zero, Vector3.forward, Color.red, .5f);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject == _playerMovement.gameObject)
            {
                var a = _playerMovement.transform.forward;
                var b = transform.forward;

                if (Input.GetAxis("Vertical") < 0 && (a - b).x < 0)
                {
                    var rotation = other.transform.rotation;
                    rotation.y = transform.rotation.y;
                    other.transform.rotation = rotation;
                }
                else if (Input.GetAxis("Vertical") > 0 && (a - b).x > 0)
                {
                    var rotation = other.transform.rotation;
                    rotation.y = transform.rotation.y;
                    other.transform.rotation = rotation;
                }
                return;
            }

            var aiMovement = other.GetComponent<AIMovement>();
            if (aiMovement != null)
            {
                var aiForward = aiMovement.transform.forward;
                var aiForward2d = new Vector2(aiForward.x, aiForward.z);

                var targetDir = transform.position - aiMovement.transform.position;
                var angle = Vector3.Angle (targetDir, aiMovement.transform.forward);
                Debug.Log(angle);
                
                if (Mathf.Abs(angle) >= 90f)
                {
                    var rotation = other.transform.rotation;
                    rotation.y = transform.rotation.y;
                    other.transform.rotation = rotation;
                }
                

            }

        }
    }

    public static class DrawArrow
    {
        public static void ForGizmo(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f,
            float arrowHeadAngle = 20.0f)
        {
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) *
                            new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) *
                           new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }

        public static void ForGizmo(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f,
            float arrowHeadAngle = 20.0f)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) *
                            new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) *
                           new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }

        public static void ForDebug(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f,
            float arrowHeadAngle = 20.0f)
        {
            Debug.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) *
                            new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) *
                           new Vector3(0, 0, 1);
            Debug.DrawRay(pos + direction, right * arrowHeadLength);
            Debug.DrawRay(pos + direction, left * arrowHeadLength);
        }

        public static void ForDebug(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f,
            float arrowHeadAngle = 20.0f)
        {
            Debug.DrawRay(pos, direction, color);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) *
                            new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) *
                           new Vector3(0, 0, 1);
            Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
        }
    }
}