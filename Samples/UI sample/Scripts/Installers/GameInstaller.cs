using Commands;
using Controllers;
using Signals;
using States;
using Views;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<HardwareBackPressSignal>();
            Container.DeclareSignal<StartSceneSignal>();

            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
            Container.Bind<PauseController>().AsSingle().NonLazy();
            Container.Bind<MenuState>().AsSingle().NonLazy();
            Container.Bind<LoadStateCommand<MenuState>>().AsSingle();

            Container.BindSignal<HardwareBackPressSignal>()
                .ToMethod<LoadStateCommand<MenuState>>(x => x.Execute).FromResolve();

            Container.BindInterfacesTo<MainView>().AsSingle();
        }
    }
}