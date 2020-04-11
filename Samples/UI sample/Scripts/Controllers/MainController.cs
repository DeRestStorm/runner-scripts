using UnityEngine;
using Zenject;

namespace Controllers
{    
    public class MainController: MonoBehaviour
    {
        [Inject] private SceneContext Context;
        private void Start()
        {
            // Context.Container.Instantiate<PausePresenter>();
        }
    }
}