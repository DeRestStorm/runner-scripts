using States;
using Zenject;

namespace Commands
{
    public class LoadStateCommand<T> where T : IState
    {
        [Inject]private DiContainer _container;
        [Inject] private IStateMachine _stateMachine { get; set; }

        public  void Execute ()
        {
            _stateMachine.Load(_container.Resolve<T>());
        }
    }
}