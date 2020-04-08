using UnityEngine;

namespace Controllers
{
    public class PauseController
    { 
        public bool IsPaused { get; private set; }

        public void Pause()
        {
            Time.timeScale = 0;
            IsPaused = true;
        }

        public void UnPause()
        {
            Time.timeScale = 1;
            IsPaused = false;
        }
    }
}