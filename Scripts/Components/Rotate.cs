using System;
using UnityEngine;

namespace Components
{
    public class Rotate : MonoBehaviour
    {
        public float xAngle, yAngle, zAngle;


        private void Update()
        {
            transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
        }
    }
}