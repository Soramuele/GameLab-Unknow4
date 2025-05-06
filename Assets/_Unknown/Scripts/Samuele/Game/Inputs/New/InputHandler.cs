using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Unknown.Samuele
{
    [CreateAssetMenu(fileName = "Inputs", menuName = "ScriptableObjects/Game Inputs")]
    public class InputHandler : ScriptableObject, GameControls.IPlayerActions, GameControls.IMinigameActions, GameControls.IUIActions
    {
        private GameControls _inputs;
        private LastInputMap lastInputMap = LastInputMap.None;
        private ActiveDevice lastControlScheme;
        private ActiveDevice controlScheme = ActiveDevice.Keyboard;

#region Events
        // Player
        public UnityAction<Vector2> MoveEvent;
        public UnityAction<Vector2> LookEvent;
        public UnityAction JumpEvent;
        public UnityAction JumpCancelledEvent;
        public UnityAction SprintEvent;
        public UnityAction SprintCancelledEvent;
        public UnityAction InteractEvent;

        // Minigame
        public UnityAction<Vector2> MovementEvent;
        public UnityAction BackEvent;

        // Player + Minigame
        public UnityAction PauseEvent;
        
        // UI
        public UnityAction CloseUIEvent;

        // Change Input Device
        public UnityAction<ActiveDevice> ChangeDeviceEvent;
#endregion Events

        void OnEnable()
        {
            if (_inputs == null)
            {
                _inputs = new GameControls();

                _inputs.Player.SetCallbacks(this);
                _inputs.Minigame.SetCallbacks(this);
                _inputs.UI.SetCallbacks(this);

                // Listener for changing control scheme
                _inputs.Player.Move.started += UpdateControlScheme;
                _inputs.Player.Interact.started += UpdateControlScheme;
                _inputs.Player.Sprint.started += UpdateControlScheme;
                _inputs.Player.Pause.started += UpdateControlScheme;
                _inputs.Minigame.Pause.started += UpdateControlScheme;
                _inputs.Minigame.Movement.started += UpdateControlScheme;
                _inputs.Minigame.Back.started += UpdateControlScheme;
                _inputs.UI.Close.started += UpdateControlScheme;

                SetGameplay();
            }
        }

        void OnDisable()
        {
            // Listener for changing control scheme
            _inputs.Player.Move.started -= UpdateControlScheme;
            _inputs.Player.Interact.started -= UpdateControlScheme;
            _inputs.Player.Sprint.started -= UpdateControlScheme;
            _inputs.Player.Pause.started -= UpdateControlScheme;
            _inputs.Minigame.Pause.started -= UpdateControlScheme;
            _inputs.Minigame.Movement.started -= UpdateControlScheme;
            _inputs.Minigame.Back.started -= UpdateControlScheme;
            _inputs.UI.Close.started -= UpdateControlScheme;
        }

#region Utilities
        public void SetGameplay()
        {
            lastInputMap = LastInputMap.Player;

            _inputs.Player.Enable();
            _inputs.Minigame.Disable();
            _inputs.UI.Disable();
        }

        public void SetMinigame()
        {
            lastInputMap = LastInputMap.MiniGame;

            _inputs.Minigame.Enable();
            _inputs.Player.Disable();
            _inputs.UI.Disable();
        }

        public void SetUI()
        {
            _inputs.UI.Enable();
            _inputs.Player.Disable();
            _inputs.Minigame.Disable();
        }

        private void UpdateControlScheme(InputAction.CallbackContext context)
        {
            var device = context.control?.device;
            if (device == null)
                return;
            
            var newScheme = device is Gamepad ? ActiveDevice.Controller : ActiveDevice.Keyboard;

            if (newScheme != lastControlScheme)
            {
                lastControlScheme = controlScheme;
                controlScheme = newScheme;
                
                Debug.Log($"Control scheme switched to {newScheme}");
                ChangeDeviceEvent?.Invoke(controlScheme);
            }
        }
#endregion Utilities

#region Player Input
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());

            UpdateControlScheme(context);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            LookEvent?.Invoke(context.ReadValue<Vector2>());

            UpdateControlScheme(context);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            InteractEvent?.Invoke();

            UpdateControlScheme(context);
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                SprintEvent?.Invoke();
            if (context.phase == InputActionPhase.Canceled)
                SprintCancelledEvent?.Invoke();

            UpdateControlScheme(context);
        }

        // This is shared with MiniGame
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();

                SetUI();
            }

            UpdateControlScheme(context);
        }
#endregion Player Input

#region MiniGame Input
        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementEvent?.Invoke(context.ReadValue<Vector2>());

            UpdateControlScheme(context);
        }

        public void OnBack(InputAction.CallbackContext context)
        {
            BackEvent?.Invoke();

            UpdateControlScheme(context);
        }
#endregion MiniGame Input

#region UI Input
        public void OnClose(InputAction.CallbackContext context)
        {
            // Check last inputMap and set it back
            switch (lastInputMap)
            {
                case LastInputMap.Player:
                    SetGameplay();
                break;
                case LastInputMap.MiniGame:
                    SetMinigame();
                break;
                default:
                    SetGameplay();
                    Debug.LogWarning("You messed up someway");
                    Debug.Break();
                break;
            }

            UpdateControlScheme(context);
        }
#endregion UI Input
    }

#region Enums
    public enum LastInputMap
    {
        None,
        Player,
        MiniGame
    }

    public enum ActiveDevice
    {
        Keyboard,
        Controller
    }
#endregion Enums
}
