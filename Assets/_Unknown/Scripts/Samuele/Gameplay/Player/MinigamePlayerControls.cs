using UnityEngine;
using UnityEngine.Events;

namespace Unknown.Samuele
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MinigamePlayerControls : PausableMonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private InputHandler inputs;

        [Header("Player Data")]
        [SerializeField] private float moveSpeed = 5f;

        private Vector2 playerMovement;
        private bool canMove;
        public bool CanMove { get => canMove; set => canMove = value; }
        private Rigidbody2D rb;

        public UnityAction DieEvent;
        public UnityAction FinishReachedEvent;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            gameObject.SetActive(false);
        }

        void OnEnable() =>
            inputs.MovementEvent += GetMovement;
        
        void OnDisable() =>
            inputs.MovementEvent -= GetMovement;

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
                    FinishReachedEvent?.Invoke();
                break;
                case "Wall":
                    DieEvent?.Invoke();
                break;
            }
        }
    }
}
