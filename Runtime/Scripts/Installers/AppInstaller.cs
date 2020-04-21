using Controllers;
using Signals;
using States;
using Zenject;

namespace Scripts.Installers
{
    public class AppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<HardwareBackPressSignal>();
            Container.DeclareSignal<StartSceneSignal>();
            Container.DeclareSignal<AddItemsSignal>();
            Container.DeclareSignal<GamePausedSignal>();
            Container.DeclareSignal<GameUnPausedSignal>();

            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
            Container.Bind<PauseController>().AsSingle().NonLazy();
        }
    }
}