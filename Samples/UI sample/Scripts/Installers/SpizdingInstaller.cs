using Commands;
using Controllers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Repositories;
using Signals;
using States;
using Zenject;

namespace Scripts.Installers
{
    public class SpizdingInstaller: MonoInstaller
    { public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<HardwareBackPressSignal>();
            Container.DeclareSignal<StartSceneSignal>();
            Container.DeclareSignal<AddItemsSignal>();
            Container.DeclareSignal<ChangeScrapSignal>();

            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
            Container.Bind<PauseController>().AsSingle().NonLazy();
            Container.Bind<MenuState>().AsSingle().NonLazy();
            Container.Bind<LoadStateCommand<MenuState>>().AsSingle();
            Container.Bind<IItemRepository<Item>>().To<ItemRepository>().AsSingle();

            Container.BindSignal<HardwareBackPressSignal>()
                .ToMethod<LoadStateCommand<MenuState>>(x => x.Execute).FromResolve();
        }
        
    }
}