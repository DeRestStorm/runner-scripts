using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components
{
    public class LoadLvl : MonoBehaviour
    {
        public void Load(int number)
        {
            SceneManager.LoadScene(number);
        }
    }
}