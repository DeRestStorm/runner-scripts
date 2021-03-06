using BezierSolution;
using Commands;
using Controllers;
using Runtime.Scripts.States;
using Scripts.Controllers.Behaviours;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Repositories;
using Signals;
using Zenject;

namespace Scripts.Installers
{
    public class SpizdingInstaller : MonoInstaller
    {
        public int Time;

        public override void InstallBindings()
        {
            Container.DeclareSignal<TheftTimerSignal>();

            Container.Bind<MenuState>().AsSingle().NonLazy();
            Container.Bind<LoadStateCommand<MenuState>>().AsSingle();
            Container.Bind<IItemRepository<Item>>().To<ItemRepository>().AsSingle();

            Container.BindSignal<HardwareBackPressSignal>()
                .ToMethod<LoadStateCommand<MenuState>>(x => x.Execute).FromResolve();
            Container.Bind<BezierWalkerWithTime>().FromComponentInHierarchy().AsSingle();

            Container.Bind<StartRunnerSceneCommand>().AsSingle();

            Container.Bind<CameraRails>().FromNewComponentOnNewGameObject().WithGameObjectName("RailsController")
                .AsSingle().NonLazy();

            Container.Bind<TimerBehaviour>().FromNewComponentOnNewGameObject().WithGameObjectName("Timer")
                .AsSingle().WithArguments(Time).NonLazy();
            
            
            var pauseController = Container.Resolve<PauseController>();
            pauseController.Reset();
        }
    }
}