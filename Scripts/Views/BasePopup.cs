using UnityEngine;

namespace Views
{
    public abstract class BasePopup : MonoBehaviour
    {
        public bool IsShowed { get { return gameObject.activeSelf; } }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}