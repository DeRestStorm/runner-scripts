using System;
using System.Globalization;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Views
{
    public class TimerView : MonoBehaviour
    {
        public Text Label;
        [Inject] private SignalBus _signalBus;

        private void Start()
        {
            _signalBus.Subscribe<TheftTimerSignal>(OnTimerUpdate);
        }

        private void OnTimerUpdate(TheftTimerSignal signal)
        {
            Label.text = signal.Time.ToString("N1");
        }
    }
}