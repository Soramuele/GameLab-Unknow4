using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerMovement;

    private InputManager inputManager;
    private Transform cameraTransform;

    [Header("Player")]
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] private float sprintSpeed = 3f;

    [Header("Physics")]
    [SerializeField] private float gravityValue = -9.81f;

    private void Start()
    {
        inputManager = InputManager.Instance;

        controller = GetComponent<CharacterController>();

        cameraTransform = Camera.main.transform;
        
        // Inputs
        inputManager.PlayerJump().performed += Jump;

    }

    void Update()
    {
        // Gravity();
        Movement();
        Sprint();
    }

    // private void Gravity()
    // {
    //     if (controller.isGrounded && playerMovement.y < 0)
    //         playerMovement.y = 0;
    // }

    private void Movement()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new(movement.x, 0, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(playerSpeed * Time.deltaTime * move);
    }

    private void Sprint()
    {
        if (inputManager.GetPlayerSprint() != 0)
        {
            // Perform sprint action
            Debug.Log("Onions");
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx);
        // Makes the player jump
        // if (ctx.performed && controller.isGrounded)
        // {
        //     playerMovement.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        // }

        // // playerMovement.y += gravityValue * Time.deltaTime;
        // controller.Move(playerMovement * Time.deltaTime);
    }
}
