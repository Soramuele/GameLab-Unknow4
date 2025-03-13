using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControls : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerMovement;
    private bool groundedPlayer;

    private InputManager inputManager;

    [Header("Player")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;

    [Header("Physics")]
    [SerializeField] private float gravityValue = -9.81f;

    private void Start()
    {
        inputManager = InputManager.Instance;

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerMovement.y < 0)
        {
            playerMovement.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new(movement.x, 0, movement.y);
        controller.Move(playerSpeed * Time.deltaTime * move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerMovement.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerMovement.y += gravityValue * Time.deltaTime;
        controller.Move(playerMovement * Time.deltaTime);
    }
}
