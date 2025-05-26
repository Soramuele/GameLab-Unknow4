using UnityEngine;
using UnityEngine.Events;

namespace Unknown.Samuele
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MinigamePlayerControls : Pausable
    {
        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;

        [Header("Player Data")]
        [SerializeField] private float moveSpeed = 5f;

        private Vector2 playerMovement;
        private bool canMove;
        public bool CanMove { get => canMove; set => canMove = value; }
        private Rigidbody2D rb;

        public UnityAction OnDieEvent;
        public UnityAction OnFinishReachedEvent;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            rb = GetComponent<Rigidbody2D>();
            gameObject.SetActive(false);
        }

        void OnEnable() =>
            inputHandler.OnMoveEvent += GetMovement;
        
        void OnDisable() =>
            inputHandler.OnMoveEvent -= GetMovement;

        // FixedUpdate is called once per fixed frame
        void FixedUpdate()
        {
            if (!canMove)
                return;
            
            // Handle movement
            rb.velocity = moveSpeed * Time.fixedDeltaTime * playerMovement;
        }

        private void GetMovement(Vector2 move) =>
            playerMovement = move;

        void OnTriggerEnter2D(Collider2D collider)
        {
            // Use objects name out of semplicity
            switch (collider.name)
            {
                case "EndBlock":
                    OnFinishReachedEvent?.Invoke();
                break;
                case "Walls":
                    OnDieEvent?.Invoke();
                break;
            }
        }
    }
}