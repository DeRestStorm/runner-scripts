using Commands;
using Controllers;
using Controllers.Behaviours;
using Factories;
using Runtime.Scripts.States;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Repositories;
using Signals;
using UnityEngine;
using Views;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public GameObject ScrapPrefab;
        public GameObject Killer;

        // [InjectOptional]
        // private IItemRepository<Item> _itemRepository = new ItemRepository();
        public override void InstallBindings()
        {
            Container.Bind<MenuState>().AsSingle().NonLazy();
            Container.Bind<LoadStateCommand<MenuState>>().AsSingle();
            // Container.BindInstance(_itemRepository);
            // Container.BindInterfacesTo<IItemRepository<Item>>().AsSingle();
            Container.Bind<IItemRepository<Item>>().To<ItemRepository>().AsSingle().NonLazy();
            Container.Bind<StartRunnerSceneCommand>().AsSingle();
            Container.Bind<PauseView>().FromComponentInHierarchy().AsSingle();

            Container.BindSignal<HardwareBackPressSignal>()
                .ToMethod<LoadStateCommand<MenuState>>(x => x.Execute).FromResolve();

            Container.BindInterfacesTo<MainView>().AsSingle();

            Container.BindFactory<Vector3, ScrapBehaviour, ScrapFactory>().FromNewComponentOnNewPrefab(ScrapPrefab);


            var pauseController = Container.Resolve<PauseController>();
            pauseController.Reset();
            var itemRepository = Container.Resolve<IItemRepository<Item>>();
            itemRepository.Load();
        }
    }
}