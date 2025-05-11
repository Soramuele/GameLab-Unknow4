using UnityEngine;

namespace Unknown.Samuele
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControls : PausableMonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private InputHandler playerInputs;
        public InputHandler PlayerInputs => playerInputs;

        [Header("Player Data")]
        [SerializeField] private float playerSpeed;
        [SerializeField] private float sprintMultiplier;
        [SerializeField] private float jumpForce;

        private CharacterController controller;
        
        private Vector2 movement;
        private float baseSprint = 1;
        private float sprint;

        private Transform cameraTransform;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            cameraTransform = Camera.main.transform;
            sprint = baseSprint;
        }

        void OnEnable()
        {
            playerInputs.MoveEvent += ctx => movement = ctx.normalized;
            // playerInputs.JumpEvent += () => Jump;
            // playerInputs.JumpCancelledEvent += () => StopJump;
            playerInputs.SprintEvent += () => sprint = sprintMultiplier;
            playerInputs.SprintCancelledEvent += () => sprint = baseSprint;
        }

        void OnDisable()
        {
            playerInputs.MoveEvent -= ctx => movement = ctx.normalized;
            // playerInputs.JumpEvent -= () => Jump;
            // playerInputs.JumpCancelledEvent -= () => StopJump;
            playerInputs.SprintEvent -= () => sprint = sprintMultiplier;
            playerInputs.SprintCancelledEvent -= () => sprint = baseSprint;
        }

        void Update()
        {
            Movement();
        }

        private void Movement()
        {
            Vector3 move = new Vector3(movement.x, 0f, movement.y);
            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;

            move *= sprint;
            move.y = -1f;
            controller.Move(playerSpeed * Time.deltaTime * move);
        }
    }
}
