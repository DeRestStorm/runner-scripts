using Scripts.Interfaces;
using Scripts.Models;
using UnityEngine.SceneManagement;
using Zenject;

namespace Commands
{
    public class StartRunnerSceneCommand
    {
        [Inject] private ZenjectSceneLoader _sceneLoader;

        public void Exequte()
        {
            _sceneLoader.LoadScene(1, LoadSceneMode.Single,
                (container) => { container.BindInstance("default_level").WhenInjectedInto<IItemRepository<Item>>(); });
        }
    }
}