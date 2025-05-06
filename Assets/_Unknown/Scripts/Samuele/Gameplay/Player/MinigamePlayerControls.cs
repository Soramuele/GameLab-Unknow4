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
        [SerializeField] private float moveSpeed;

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
            inputs.SetMinigame();
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
            var _moveX = playerMovement.x * moveSpeed;
            var _moveY = playerMovement.y * moveSpeed;

            rb.velocity = new Vector2(_moveX, _moveY);
        }

        private void GetMovement(Vector2 move) =>
            playerMovement = move;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!canMove) return;

            // Use objects name out of semplicity
            switch (collision.name)
            {
                case "EndBlock":
                    FinishReachedEvent?.Invoke();
                break;
                case "Wall":
                    Debug.Log("Colliding with Wall");
                    DieEvent?.Invoke();
                    canMove = false;
                    rb.velocity = Vector2.zero;
                break;
            }
        }
    }
}
