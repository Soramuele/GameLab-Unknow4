using UnityEngine.Events;

namespace Unknown.Samuele
{
    public class GameplayManager
    {
#region Pause
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
#endregion Pause

#region Controls
        public enum ActiveDevice
        {
            Keyboard,
            Controller
        }

        public ActiveDevice currentControlScheme { get; private set; } = ActiveDevice.Keyboard;
        public void ChangeActiveDevice(ActiveDevice device) =>
            currentControlScheme = device;
#endregion Controls
    }
}
