using System;
using Signals;
using UnityEngine;
using Zenject;

namespace Views
{
    public class MainView : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus { get;}

        private void Start()
        {
            _signalBus.Fire<StartSceneSignal>();
        }

        private void OnApplicationQuit()
        {
            // Signal
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                _signalBus.Fire<HardwareBackPressSignal>();
                Debug.Log("Fire HardwareBackPressSignal");
            }
        }
    }
}