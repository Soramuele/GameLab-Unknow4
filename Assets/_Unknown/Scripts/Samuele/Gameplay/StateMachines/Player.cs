using UnityEngine;
using StateMachine;

namespace Unknown.Samuele
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : StateManager<Player.PlayerStates>
    {
        public enum PlayerStates
        {
            Idle,
            Walk,
            Run,
            Jump
        }

        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputs;

        [Header("Movement")]
        [SerializeField] private float speed = 4.5f;
        [SerializeField] private float runSpeedMultiplier = 2.75f;

        [Header("Physics")]
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpForce = 3f;

        [Header("Stimuli")]
        [SerializeField] private float stimuliThreshold = 32.5f;
        [SerializeField, Range(0, 1)] private float slowdownMultiplier = 0.5f;

        private CharacterController controller;
        private Camera cam;

        private Vector2 playerMovement;
        private bool isRunning;
        private bool isJumpPressed;

        // Getters
        public Vector2 PlayerMovement => playerMovement;
        public bool IsRunning { get => isRunning; set => isRunning = value; }
        public float Speed => speed;
        public float RunSpeedMultiplier => runSpeedMultiplier;
        public float Gravity => gravity;
        public float JumpForce => jumpForce;
        public bool IsJumpPressed { get => isJumpPressed; set => isJumpPressed = value; }
        public float StimuliThreshold => stimuliThreshold;
        public float SlowdownMultiplier => slowdownMultiplier;
        public CharacterController Controller => controller;
        public Camera Cam => cam;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            cam = Camera.main;

            InitializeStates();

            currentState = states[PlayerStates.Idle];
        }

        void OnEnable()
        {
            inputs.OnMovementEvent += GetMovement;
            inputs.OnSprintEvent += () => GetRunning(true);
            inputs.OnSprintCancelledEvent += () => GetRunning(false);
            inputs.OnJumpEvent += GetJump;
        }

        void OnDisable()
        {
            inputs.OnMovementEvent -= GetMovement;
            inputs.OnSprintEvent -= () => GetRunning(true);
            inputs.OnSprintCancelledEvent -= () => GetRunning(false);
            inputs.OnJumpEvent -= GetJump;
        }

        protected override void InitializeStates()
        {
            states.Add(PlayerStates.Idle, new PlayerIdleState(PlayerStates.Idle, this));
            states.Add(PlayerStates.Walk, new PlayerWalkState(PlayerStates.Walk, this));
            states.Add(PlayerStates.Run, new PlayerRunState(PlayerStates.Run, this));
            states.Add(PlayerStates.Jump, new PlayerJumpState(PlayerStates.Jump, this));
        }

        private void GetMovement(Vector2 ctx) =>
            playerMovement = ctx;

        private void GetRunning(bool value) =>
            isRunning = value;

        private void GetJump() =>
            isJumpPressed = true;
    }
}
