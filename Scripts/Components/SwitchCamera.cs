using System;
using Cinemachine;
using UnityEngine;


namespace Components
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraSwitch : MonoBehaviour
    {
        private CinemachineVirtualCamera _camera;
        public bool ActiveOnStart = false;

        private void Start()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            if (ActiveOnStart)
                Switch();
        }

        public void Switch()
        {
            _camera.Priority = 12;
            foreach (var cineMachineVirtualCamera in FindObjectsOfType<CinemachineVirtualCamera>())
            {
                if (cineMachineVirtualCamera == _camera)
                    continue;
                cineMachineVirtualCamera.Priority = 10;
            }
        }
    }
}