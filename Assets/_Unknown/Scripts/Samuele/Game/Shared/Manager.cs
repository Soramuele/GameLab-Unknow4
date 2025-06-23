using UnityEngine;

namespace Unknown.Samuele
{
    public abstract class Manager : MonoBehaviour
    {
        private bool isSubscribed = false;

        protected virtual void Start() =>
            Subscribe();

        protected virtual void OnDestroy() =>
            Unsubscribe();

        private void Subscribe()
        {
            // GameManager.AddManager(this);
            isSubscribed = true;
        }

        private void Unsubscribe()
        {
            // if (isSubscribed)
            //     GameManager.RemoveManager(this);
        }

        public virtual void Save() { }
        public virtual void Load() { }
        protected virtual void Pause() { }
        protected virtual void Resume() { }
    }
}
