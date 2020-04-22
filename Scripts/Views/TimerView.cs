using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Views
{
    public class TimerView : MonoBehaviour
    {
        private Text _label;
        [Inject] private SignalBus _signalBus;

        private void Start()
        {
            _label = GetComponent<Text>();
            _signalBus.Subscribe<TheftTimerSignal>(OnTimerUpdate);
        }

        private void OnTimerUpdate(TheftTimerSignal signal)
        {
            _label.text = signal.Time.ToString("N1");
        }
    }
}