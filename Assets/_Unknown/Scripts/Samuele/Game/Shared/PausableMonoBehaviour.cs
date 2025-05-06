using UnityEngine;

namespace Unknown.Samuele
{
    public abstract class PausableMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private bool pausable;

        protected virtual void Awake()
        {
            // GameManager.Instance.GetGameplay().onPause += OnPause;
            // GameManager.Instance.GetGameplay().onResume += OnResume;
        }

        public void OnPause()
        {
            if (!pausable)
                return;
            
            enabled = false;

            PostPause();
        }

        public void OnResume()
        {
            if (!pausable)
                return;
            
            enabled = true;

            PostResume();
        }

        // Use for coroutines and other function that cannot be stopped when disabling the script
        protected virtual void PostPause() {   }
        protected virtual void PostResume() {   }
    }
}
