using UnityEngine;

namespace Unknown.Samuele
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : Pausable
    {
        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;

        [Header("Player")]
        [SerializeField] private float playerSpeed = 5.0f;
        [SerializeField] private float sprintSpeed = 3f;
        private Vector2 playerMovement = new Vector2(0, 0);
        private float sprint = 1;
        
        private CharacterController controller;
        private Camera cam;

        protected override void Start()
        {
            base.Start();

            controller = GetComponent<CharacterController>();

            cam = Camera.main;
        }

        void OnEnable()
        {
            inputHandler.OnMovementEvent += ctx => playerMovement = ctx;
            inputHandler.OnSprintEvent += () => sprint = sprintSpeed;
            inputHandler.OnSprintCancelledEvent += () => sprint = 1f;
        }

        void OnDisable()
        {
            inputHandler.OnMovementEvent -= ctx => playerMovement = ctx;
            inputHandler.OnSprintEvent -= () => sprint = sprintSpeed;
            inputHandler.OnSprintCancelledEvent -= () => sprint = 1f;
        }

        void Update()
        {
            Movement();
        }

        private void Movement()
        {
            Vector3 move = new Vector3(playerMovement.x, 0, playerMovement.y);
            move *= sprint;
            move = cam.transform.forward * move.z + cam.transform.right * move.x;
            move.y = -1f;

            controller.Move(playerSpeed * Time.deltaTime * move);
        }
    }
}
