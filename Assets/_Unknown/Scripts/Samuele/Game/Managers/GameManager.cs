using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Unknown.Samuele
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;

        public enum CurrentDevice
        {
            Keyboard_Mouse,
            XBoxController,
            PlayStationController
        }
        private CurrentDevice currentDevice = CurrentDevice.Keyboard_Mouse;
        public enum InputMap
        {
            None,
            Gameplay,
            Minigame,
            UI
        }

        private bool paused = false;

#region Events
        public UnityAction<CurrentDevice> OnChangeDeviceEvent;

        public UnityAction OnPauseEvent;
        public UnityAction OnResumeEvent;
#endregion Events

        void Awake()
        {
            Instance = this;
        }

        void OnEnable()
        {
            inputHandler.OnChangeDeviceEvent += ChangeCurrentDevice;
            inputHandler.OnPauseEvent += PauseGame;
            inputHandler.OnResumeEvent += ResumeGame;
        }

        void OnDisable()
        {
            inputHandler.OnChangeDeviceEvent -= ChangeCurrentDevice;
            inputHandler.OnPauseEvent -= PauseGame;
            inputHandler.OnResumeEvent -= ResumeGame;
        }

        private void ChangeCurrentDevice(string device)
        {
            switch (device)
            {
                case "Keyboard":
                case "Mouse":
                    currentDevice = CurrentDevice.Keyboard_Mouse;
                    break;
                case "XInputControllerWindows":
                    currentDevice = CurrentDevice.XBoxController;
                    break;
                case "DualShock4GamepadHID":
                case "DualSenseGamepadHID":
                    currentDevice = CurrentDevice.PlayStationController;
                    break;
            }

            Debug.Log($"Controls changed to {currentDevice}");

            OnChangeDeviceEvent?.Invoke(currentDevice);
        }

        public void ChangeInputMap(InputMap map)
        {
            switch (map)
            {
                case InputMap.None:
                    inputHandler.DisableAllInputs();
                    break;
                case InputMap.Gameplay:
                    inputHandler.SetGameplay();
                    break;
                case InputMap.Minigame:
                    inputHandler.SetMinigame();
                    break;
                case InputMap.UI:
                    Debug.Log("Not Implemented yet");
                    break;
            }
        }

        private void PauseGame()
        {
            paused = true;
            OnPauseEvent?.Invoke();
        }

        private void ResumeGame()
        {
            paused = false;
            OnResumeEvent?.Invoke();
        }

        public void ChangeScene(SceneReference scene, LoadSceneMode mode)
        {
            SceneManager.LoadScene(scene, mode);
        }

        public void HideUI()
        {
            GetComponentInChildren<InteractMessage>().Hide();
            GetComponentInChildren<CrosshairManager>().Hide();
        }

        public void ShowUI()
        {
            GetComponentInChildren<InteractMessage>().Show();
            GetComponentInChildren<CrosshairManager>().Show();
        }
    }
}
