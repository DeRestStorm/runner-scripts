using System.Collections;
using System.Collections.Generic;
using Commands;
using Runtime.Scripts.Commands;
using Signals;
using UnityEngine;
using Zenject;

namespace Scripts.Controllers.Behaviours
{
    public class TimerBehaviour : MonoBehaviour
    {
        private float _time;
        [Inject] private SignalBus _signalBus;
        private bool _paused;
        [Inject] private StartRunnerSceneCommand _startRunnerSceneCommand;

        [Inject]
        public void Construct(int value)
        {
            _time = value;
        }

        private void Start()
        {
            _signalBus.Subscribe<GamePausedSignal>(OnPaused);
            _signalBus.Subscribe<GameUnPausedSignal>(OnUnPaused);

            StartCoroutine(nameof(Cycle));
        }

        private IEnumerator Cycle()
        {
            while (true)
            {
                yield return new WaitForSeconds(.1f);
                if (_paused) continue;

                _time -= .1f;
                _signalBus.Fire(new TheftTimerSignal() {Time = _time});
                
                if (_time > 0) continue;

                _startRunnerSceneCommand.Exequte();
                yield break;
            }
        }


        private void OnPaused()
        {
            _paused = true;
        }

        private void OnUnPaused()
        {
            _paused = false;
        }
    }
}