using Commands;
using Runtime.Scripts.Commands;
using Signals;
using States;
using Zenject;

namespace Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<HardwareBackPressSignal>();
            Container.DeclareSignal<StartSceneSignal>();

            Container.Bind<MainMenuState>().AsSingle().NonLazy();
            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
            Container.Bind<LoadStateCommand<MainMenuState>>().AsSingle();
            Container.Bind<StartRunnerSceneCommand>().AsSingle();
            

            Container.BindSignal<StartSceneSignal>()
                .ToMethod<LoadStateCommand<MainMenuState>>(x => x.Execute).FromResolve();
        }
    }
}