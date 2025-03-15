using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance {
        get { 
            return _instance; 
        } 
    }

    private PlayerControls PlayerControls;

    private void Awake()
    {
        if (PlayerControls != null && _instance != this ) {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        PlayerControls = new PlayerControls();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return PlayerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return PlayerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumpedThisFrame()
    {
        return PlayerControls.Player.Jump.triggered;
    }
}