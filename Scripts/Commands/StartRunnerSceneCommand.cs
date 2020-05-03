using Installers;
using Scripts.Interfaces;
using Scripts.Models;
using UnityEngine.SceneManagement;
using Zenject;

namespace Runtime.Scripts.Commands
{
    public class StartRunnerSceneCommand
    {
        [Inject] private ZenjectSceneLoader _sceneLoader;
        [Inject] private IItemRepository<Item> _itemRepository;

        public void Exequte()
        {
            
            
            SceneManager.LoadScene(1);
            // _sceneLoader.LoadScene(1, LoadSceneMode.Single,
            //     (container) => { container.BindInstance(_itemRepository).WhenInjectedInto<GameInstaller>(); });
        }
    }
}