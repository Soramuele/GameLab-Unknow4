using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
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
    
    public void PauseGame()
    {
        inputActions.Player.Disable();
        inputActions.UI.Enable();
    }
#endregion

#region UI Inputs
    public bool UISelect() =>
        inputActions.UI.Select.triggered;

    public void UnpauseGame()
    {
        inputActions.UI.Disable();
        inputActions.Player.Enable();
    }
    #endregion
}