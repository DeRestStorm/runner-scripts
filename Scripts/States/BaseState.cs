using System.Resources;
using Signals;
using UnityEngine;
using Zenject;

namespace States
{
    public abstract class BaseState : IState
    {
        [Inject] private IStateMachine _stateMachine { get; set; }
        [Inject] readonly SignalBus _signalBus;

        public virtual void Load()
        {
            _signalBus.Subscribe<HardwareBackPressSignal>(OnHardwareBackPress);
        }

        public virtual void Unload()
        {
            _signalBus.Unsubscribe<HardwareBackPressSignal>(OnHardwareBackPress);
            OnHardwareBackPress();
        }
        
        protected virtual void OnHardwareBackPress()
        {
            if (_stateMachine.LastState != this)
                return;

            _stateMachine.Unload(false);
        }
    }
}