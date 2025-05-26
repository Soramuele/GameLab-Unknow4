using UnityEngine;

namespace Unknown.Samuele
{
    public abstract class Pausable : MonoBehaviour
    {
        [Tooltip("Check this box if you want this component to be paused when the game pauses")]
        [SerializeField] private bool pausable;

        protected virtual void Start()
        {
            GameManager.Instance.OnPauseEvent += OnPause;
            GameManager.Instance.OnResumeEvent += OnResume;
        }

        protected virtual void OnDestroy()
        {
            GameManager.Instance.OnPauseEvent -= OnPause;
            GameManager.Instance.OnResumeEvent -= OnResume;
        }

        private void OnPause()
        {
            if (!pausable)
                return;

            enabled = false;

            PostPause();
        }

        private void OnResume()
        {
            if (!pausable)
                return;

            enabled = true;

            PostResume();
        }

        // Use for coroutines and other functions that cannot be stopped when disabling the script
        protected virtual void PostPause() {    }
        protected virtual void PostResume() {   }
    }
}
