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

        [Header("Title Screen")]
        [SerializeField] private SceneReference titleScreen;

        private CurrentDevice currentDevice = CurrentDevice.Keyboard_Mouse;
        private InputMap currentInputMap = InputMap.UI;
        private InputMap previousInputMap;

        public CurrentDevice CurrentDevice => currentDevice;
        public InputMap CurrentInputMap => currentInputMap;

#region Events
        public UnityAction<CurrentDevice> OnChangeDeviceEvent;

        public UnityAction OnPauseEvent;
        public UnityAction OnResumeEvent;
#endregion Events

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        void Start()
        {
            ChangeInputMap(InputMap.Gameplay);
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

#region Inputs
        private void ChangeCurrentDevice(string device)
        {
            switch (device)
            {
                case "Mouse":
                case "Keyboard":
                    currentDevice = CurrentDevice.Keyboard_Mouse;
                    break;
                case "XInputControllerWindows":
                    currentDevice = CurrentDevice.XBoxController;
                    break;
                case "DualShock4GamepadHID":
                case "DualSenseGamepadHID":
                    currentDevice = CurrentDevice.PlayStationController;
                    break;
                default:
                    Debug.Log($"Another device? {device}");
                    currentDevice = CurrentDevice.Keyboard_Mouse;
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
                    previousInputMap = currentInputMap;
                    currentInputMap = InputMap.Gameplay;
                    break;
                case InputMap.Minigame:
                    inputHandler.SetMinigame();
                    previousInputMap = currentInputMap;
                    currentInputMap = InputMap.Minigame;
                    break;
                case InputMap.UI:
                    inputHandler.SetUI();
                    previousInputMap = currentInputMap;
                    currentInputMap = InputMap.UI;
                    break;
            }
        }
#endregion Inputs

#region Pause System
        private void PauseGame()
        {
            OnPauseEvent?.Invoke();
            ChangeInputMap(InputMap.UI);
        }

        private void ResumeGame()
        {
            OnResumeEvent?.Invoke();
            ChangeInputMap(previousInputMap);
        }

        public void ResumeFromSettings()
        {
            ResumeGame();
        }
#endregion Pause System

#region Scene Management
        public void LoadSceneAsync(SceneReference scene)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }

        public void UnloadSceneAsync(SceneReference scene)
        {
            SceneManager.UnloadSceneAsync(scene);
        }

        public void BackToTitleScreen()
        {
            SceneManager.LoadSceneAsync(titleScreen);
        }
#endregion Scene Management
    }
}
