using UnityEngine;
using UnityEngine.Events;

namespace Unknown.Samuele
{
    public class MazePlayer : Pausable
    {
        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;

        [Header("Movement")]
        [SerializeField] private float speed = 10f;

        [Header("Audios")]
        [SerializeField] private SOAudio audios;

        private Rigidbody2D rb;
        private Vector2 movement;

        private AudioManager audioManager;

        public UnityAction OnWinEvent;
        public UnityAction OnDieEvent;

        protected override void Start()
        {
            base.Start();

            rb = GetComponent<Rigidbody2D>();

            audioManager = AudioManager.Instance;
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
            {
                audioManager.PlaySFX(audios.SFXClips["Die"][0], transform.position);
                OnDieEvent?.Invoke();
            }
            else if (collider.CompareTag("Scoring"))
            {
                audioManager.PlaySFX(audios.SFXClips["Score"][0], transform.position);
                OnWinEvent?.Invoke();
            }
        }
    }
}
