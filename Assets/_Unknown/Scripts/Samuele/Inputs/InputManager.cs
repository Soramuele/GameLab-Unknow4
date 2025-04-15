using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unknown.Samuele
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        private PlayerInput inputActions;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            inputActions = new();
        }

        void OnEnable()
        {
            inputActions.Player.Enable();

            inputActions.Player.Pause.started += PauseGame;
            inputActions.Minigame.Back.started += UnpauseGame;
        }

        void OnDisable()
        {
            inputActions.Player.Disable();

            inputActions.Player.Pause.started -= PauseGame;
            inputActions.Minigame.Back.started -= UnpauseGame;
        }

    #region Player Inputs
        public Vector2 GetPlayerMovement() =>
            inputActions.Player.Move.ReadValue<Vector2>();

        public float GetPlayerSprint() =>
            inputActions.Player.Sprint.ReadValue<float>();

        public InputAction PlayerJump() =>
            inputActions.Player.Jump;

        public InputAction PlayerInteract() =>
            inputActions.Player.Interact;
        
        public void PauseGame(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                inputActions.Player.Disable();
                inputActions.Minigame.Enable();
            }

            OpenPauseMenu();
        }
    #endregion

    #region UI Inputs
        public bool UISelect() =>
            inputActions.Minigame.Back.triggered;

        public void UnpauseGame(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                inputActions.Minigame.Disable();
                inputActions.Player.Enable();
            }

            ClosePauseMenu();
        }
        #endregion

        private void OpenPauseMenu()
        {
            Debug.Log("Game is Paused");
            FindObjectOfType<CinemachineInputProvider>().enabled = false;
        }

        private void ClosePauseMenu()
        {
            Debug.Log("Game is NOT paused anymore");
            FindObjectOfType<CinemachineInputProvider>().enabled = true;
        }
    }
}
