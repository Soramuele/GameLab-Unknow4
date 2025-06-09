using UnityEngine;
using Unknown.Samuele;
using Unknown.Samuele.Inputs;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : Pausable
{
    public float playerSpeed = 2.0f;
    [SerializeField, Range(1f, 5f)] private float sprintMultiplier = 2;
    [SerializeField]
    private float jumpHeight = 0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField] private InputHandler inputs;
   
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private Vector2 movement = new Vector2();
    private float sprint = 1f;
    
        
    protected override void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    void OnEnable()
    {
        inputs.OnMovementEvent += ctx => movement = ctx;
        inputs.OnSprintEvent += () => sprint = 3f;
        inputs.OnSprintCancelledEvent += () => sprint = 1f;
    }

    void OnDisable()
    {
        inputs.OnMovementEvent -= ctx => movement = ctx;
        inputs.OnSprintEvent -= () => sprint = 3f;
        inputs.OnSprintCancelledEvent -= () => sprint = 1f;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;

        move *= sprint;

        move.y = 0f;
        controller.Move(playerSpeed * Time.deltaTime * move);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
