using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private InputActionManager inputActions;

    void Awake()
    {
        inputActions = new InputActionManager();
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

    // public bool GetPlayerSprint(){
    //    return inputActions.Player.Sprint.ReadValue<bool>(); 
    // }

    public bool GetPlayerInteract() =>
        inputActions.Player.Interact.triggered;
    
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
