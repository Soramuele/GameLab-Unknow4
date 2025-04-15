using UnityEngine.Events;

namespace Unknown.Samuele
{
    public class GameplayManager
    {
        private bool paused;
        public bool IsPaused { get => paused; }

        // Events
        public UnityAction onPause;
        public UnityAction onResume;

        public void Pause()
        {
            paused = true;
            onPause?.Invoke();
        }

        public void Resume()
        {
            paused = false;
            onResume?.Invoke();
        }
    }
}
