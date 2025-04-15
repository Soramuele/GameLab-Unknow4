using UnityEngine;

namespace Unknown.Samuele
{
    public abstract class PausableMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private bool pausable;

        private bool initialized = false;

        protected virtual void Awake()
        {
            GameManager.Instance.GetGameplay().onPause += OnPause;
            GameManager.Instance.GetGameplay().onResume += OnResume;

            initialized = true;
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
