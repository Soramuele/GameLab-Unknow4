using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unknown.Samuele
{
    [CreateAssetMenu(fileName = "Inputs", menuName = "ScriptableObjects/Game Inputs")]
    public class InputHandler : ScriptableObject, GameControls.IPlayerActions, GameControls.IMinigameActions, GameControls.IUIActions
    {
        private GameControls _inputs;
        private LastInputMap lastInputMap = LastInputMap.None;

#region Events
        // Player
        public event Action<Vector2> MoveEvent;
        public event Action JumpEvent;
        public event Action JumpCancelledEvent;
        public event Action SprintEvent;
        public event Action SprintCancelledEvent;
        public event Action InteractEvent;

        // Minigame
        public event Action<Vector2> MovementEvent;
        public event Action BackEvent;

        // Player + Minigame
        public event Action PauseEvent;
        
        // UI
        public event Action CloseUIEvent;
#endregion Events

        public Vector2 GetLook { get; private set; }
        private string lastControlScheme;
        public string currentControlScheme { get; private set; } = "Keyboard";

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
            
            string newScheme = device is Gamepad ? "Controller" : "Keyboard";

            if (newScheme != lastControlScheme)
            {
                lastControlScheme = newScheme;
                currentControlScheme = newScheme;
                Debug.Log($"Control scheme switched to {newScheme}");
            }
        }
#endregion Utilities

#region Player
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            GetLook = context.ReadValue<Vector2>();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            InteractEvent?.Invoke();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                SprintEvent?.Invoke();
            if (context.phase == InputActionPhase.Canceled)
                SprintCancelledEvent?.Invoke();
        }

        // This is shared with MiniGame
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();

                SetUI();
            }
        }
#endregion Player

#region MiniGame
        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnBack(InputAction.CallbackContext context)
        {
            BackEvent?.Invoke();
        }
#endregion MiniGame

#region UI
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
        }
#endregion UI
    }

    public enum LastInputMap
    {
        None,
        Player,
        MiniGame
    }
}
