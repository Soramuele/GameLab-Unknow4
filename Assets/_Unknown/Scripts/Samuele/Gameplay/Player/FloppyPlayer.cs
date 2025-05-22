using UnityEngine;

namespace Unknown.Samuele
{
    public class FloppyPlayer : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;

        [Header("Player")]
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float tilt = 5f;

        [Header("Physics")]
        [SerializeField] private float gravity = -9.81f;

        private SpriteRenderer spriteRenderer;
        private Vector3 direction;
        private int spriteIndex;

        private bool isPaused = false;
        public bool Pause() => isPaused = true;
        public bool Resume() => isPaused = false;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        }

        void OnEnable()
        {
            inputHandler.OnJumpEvent += () => direction = Vector3.up * jumpForce;

            var position = transform.position;
            position.y = 0f;
            transform.position = position;
            direction = Vector3.zero;
        }

        void OnDisable()
        {
            inputHandler.OnJumpEvent -= () => direction = Vector3.up * jumpForce;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPaused)
            {
                direction = Vector3.zero;
                return;
            }
            
            // Apply gravity and update the position
            direction.y += gravity * Time.deltaTime;
            transform.position += direction * Time.deltaTime;

            // Tilt the bird based on the direction
            Vector3 rotation = transform.eulerAngles;
            rotation.z = direction.y * tilt;
            transform.eulerAngles = rotation;
        }

        void AnimateSprite()
        {
            spriteIndex++;

            if (spriteIndex >= sprites.Length)
            {
                spriteIndex = 0;
            }

            if (spriteIndex < sprites.Length && spriteIndex >= 0)
            {
                spriteRenderer.sprite = sprites[spriteIndex];
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
                FloppyManager.Instance.GameOver();
            else if (other.gameObject.CompareTag("Scoring"))
                FloppyManager.Instance.IncreaseScore();
        }
    }
}
