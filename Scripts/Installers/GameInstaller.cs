using Commands;
using Controllers;
using Runtime.Scripts.Commands;
using Runtime.Scripts.States;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Repositories;
using Signals;
using Views;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuState>().AsSingle().NonLazy();
            Container.Bind<LoadStateCommand<MenuState>>().AsSingle();
            Container.Bind<IItemRepository<Item>>().To<ItemRepository>().AsSingle();
            Container.Bind<StartRunnerSceneCommand>().AsSingle();

            Container.BindSignal<HardwareBackPressSignal>()
                .ToMethod<LoadStateCommand<MenuState>>(x => x.Execute).FromResolve();

            Container.BindInterfacesTo<MainView>().AsSingle();

            var pauseController = Container.Resolve<PauseController>();
            pauseController.Reset();
        }
    }
}