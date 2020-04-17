using System;
using UnityEngine;
using Zenject;

namespace Controllers.Behaviours
{
    public class MoveWithMouse : MonoBehaviour
    {
        private bool _isPressed;

        [Inject] private Camera _camera;
        public Rigidbody Rigidbody;
        public LayerMask Mask;
        private Bounds _bounds;


        private void FixedUpdate()
        {
            if (_isPressed)
            {
                var MyRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(MyRay, out hit, 100, Mask))

                {
                    var point = hit.point;
                    point += (_bounds.max.y + .1f) * hit.normal;

                    Rigidbody.MovePosition(point);


                    // Find the line from the gun to the point that was clicked.
                    Vector3 incomingVec = hit.point - gameObject.transform.position;

                    // Use the point's normal to calculate the reflection vector.
                    Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
                    Rigidbody.MoveRotation(Quaternion.LookRotation(hit.normal, Vector3.up));

                    // Draw lines to show the incoming "beam" and the reflection.
                    Debug.DrawLine(gameObject.transform.position, hit.point, Color.red);
                    Debug.DrawRay(hit.point, reflectVec, Color.green);
                }
            }
        }

        private void OnMouseDown()
        {
            _bounds = GetComponent<MeshFilter>().sharedMesh.bounds;
            _isPressed = true;
            Rigidbody.isKinematic = true;
            gameObject.layer = 8;
            Rigidbody.drag = 10;
        }

        private void OnMouseUp()
        {
            Rigidbody.drag = 0;
            gameObject.layer = 0;
            _isPressed = false;
            Rigidbody.isKinematic = false;
        }
    }
}