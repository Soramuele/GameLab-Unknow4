using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Unknown.Samuele.Inputs
{
    [CreateAssetMenu(fileName = "Inputs", menuName = "ScriptableObjects/GameInputs")]
    public class InputHandler : ScriptableObject, GameInputs.IGameplayActions, GameInputs.IMinigameActions, GameInputs.IUIActions
    {
        private GameInputs inputActions;

#region Events
        // Gameplay
        public UnityAction<Vector2> OnMovementEvent;
        public UnityAction<Vector2> OnLookEvent;
        public UnityAction OnSprintEvent;
        public UnityAction OnSprintCancelledEvent;
        public UnityAction OnInteractEvent;

        //Minigame
        public UnityAction<Vector2> OnMoveEvent;
        public UnityAction OnJumpEvent;
        public UnityAction OnBackEvent;

        //Shared
        public UnityAction OnPauseEvent;

        // UI
        public UnityAction OnResumeEvent;

        // Change of input device
        public UnityAction<string> OnChangeDeviceEvent;
#endregion Events

        private enum PreviousInput {
            Gameplay,
            Minigame
        }
        private PreviousInput previousInput;
        private InputDevice lastInputDevice;
        
        void OnEnable()
        {
            if (inputActions == null)
            {
                // Instantiate Inputs from the Input asset
                inputActions = new GameInputs();

                // Subscribe to input maps callbacks
                inputActions.Gameplay.AddCallbacks(this);
                inputActions.Minigame.AddCallbacks(this);
                inputActions.UI.AddCallbacks(this);

                // Start game with the main action map enabled
                SetGameplay();
            }
        }

#region Gameplay Inputs
        public void OnMovement(InputAction.CallbackContext ctx)
        {
            OnMovementEvent?.Invoke(ctx.ReadValue<Vector2>());

            CheckSwitchDevice(ctx);
        }

        public void OnSprint(InputAction.CallbackContext ctx)
        {
            switch (ctx.phase)
            {
                case InputActionPhase.Performed:
                    OnSprintEvent?.Invoke();

                    CheckSwitchDevice(ctx);
                    break;
                case InputActionPhase.Canceled:
                    OnSprintCancelledEvent?.Invoke();
                    break;
            }
        }

        public void OnLook(InputAction.CallbackContext ctx)
        {
            OnLookEvent?.Invoke(ctx.ReadValue<Vector2>());

            CheckSwitchDevice(ctx);
        }

        public void OnInteract(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                OnInteractEvent?.Invoke();

                CheckSwitchDevice(ctx);
            }
        }

        // Shared between Gameplay and Minigame inputs
        public void OnPause(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                OnPauseEvent?.Invoke();

                SetUI();
            }
        }
#endregion Gameplay Inputs

#region Minigame Inputs
        public void OnMove(InputAction.CallbackContext ctx)
        {
            OnMoveEvent?.Invoke(ctx.ReadValue<Vector2>());

            CheckSwitchDevice(ctx);
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                OnJumpEvent?.Invoke();

                CheckSwitchDevice(ctx);
            }
        }

        public void OnBack(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                OnBackEvent?.Invoke();

                CheckSwitchDevice(ctx);

                SetGameplay();
            }
        }
#endregion Minigame Inputs

#region UI Inputs
        public void OnResume(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                OnResumeEvent?.Invoke();
                
                CheckSwitchDevice(ctx);

                SetPreviousInputMap();
            }
        }
#endregion UI Inputs

#region Utilities
        public void SetGameplay()
        {
            Debug.Log("Gameplay enabled");
            inputActions.Gameplay.Enable();
            inputActions.Minigame.Disable();
            inputActions.UI.Disable();

            previousInput = PreviousInput.Gameplay;
        }

        public void SetMinigame()
        {
            Debug.Log("Minigame enabled");
            inputActions.Minigame.Enable();
            inputActions.Gameplay.Disable();
            inputActions.UI.Disable();

            previousInput = PreviousInput.Minigame;
        }

        private void SetUI()
        {
            inputActions.UI.Enable();
            inputActions.Gameplay.Disable();
            inputActions.Minigame.Disable();
        }

        public void DisableInputs()
        {
            inputActions.Gameplay.Disable();
            inputActions.Minigame.Disable();
            inputActions.UI.Disable();
        }

        private void SetPreviousInputMap()
        {
            switch (previousInput)
            {
                case PreviousInput.Gameplay:
                    SetGameplay();
                    break;
                case PreviousInput.Minigame:
                    SetMinigame();
                    break;
                default:
                    Debug.Log("No previous input. What to do?");
                    break;
            }
        }

        private void CheckSwitchDevice(InputAction.CallbackContext ctx)
        {
            var device = ctx.control.device;

            if (device == lastInputDevice)
                return;

            lastInputDevice = device;

            OnChangeDeviceEvent?.Invoke(device.name);
        }
#endregion Utilities
    }
}
