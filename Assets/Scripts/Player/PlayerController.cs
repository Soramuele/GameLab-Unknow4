using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField, Range(1f, 5f)] private float sprintMultiplier = 2;
    [SerializeField]
    private float jumpHeight = 0f;
    [SerializeField]
    private float gravityValue = -9.81f;

   
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;
    
        
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;

        float sprint = inputManager.GetPlayerSprint();
        if (sprint != 0)
            move *= sprintMultiplier;

        move.y = -1f;
        controller.Move(move * Time.deltaTime * playerSpeed);
       /* if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        } */

        // Makes the player jump
        if (inputManager.PlayerJump().triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
