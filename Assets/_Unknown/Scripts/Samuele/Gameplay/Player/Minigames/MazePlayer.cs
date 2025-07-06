using UnityEngine;

namespace Unknown.Samuele
{
    public class MazePlayer : Pausable
    {
        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;

        [Header("Movement")]
        [SerializeField] private float speed = 10f;

        private Rigidbody2D rb;
        private Vector2 movement;

        protected override void Start()
        {
            base.Start();

            rb = GetComponent<Rigidbody2D>();
        }

        void OnEnable()
        {
            inputHandler.OnMoveEvent += GetMovement;
        }

        void OnDisable()
        {
            inputHandler.OnMoveEvent -= GetMovement;
        }

        void FixedUpdate()
        {
            rb.velocity = speed * Time.fixedDeltaTime * movement;
        }

        private void GetMovement(Vector2 value)
        {
            movement = value;
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Obstacle"))
                Debug.Log("player is dead");
            else if (collider.CompareTag("Scoring"))
                Debug.Log("player reached finish");
        }
    }
}
