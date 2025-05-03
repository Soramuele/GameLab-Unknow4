using UnityEngine;

namespace Unknown.Samuele
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MinigamePlayerControls : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private InputHandler inputs;

        [Header("Player Data")]
        [SerializeField] private float moveSpeed;

        private Vector2 playerMovement;
        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void OnEnable() =>
            inputs.MovementEvent += GetMovement;
        
        void OnDisable() =>
            inputs.MovementEvent -= GetMovement;

        // Update is called once per fixed frame
        void FixedUpdate()
        {
            // Handle movement
            var _moveX = playerMovement.x * moveSpeed;
            var _moveY = playerMovement.y * moveSpeed;

            rb.velocity = new Vector2(_moveX, _moveY);
        }

        private void GetMovement(Vector2 move) =>
            playerMovement = move;
    }
}
