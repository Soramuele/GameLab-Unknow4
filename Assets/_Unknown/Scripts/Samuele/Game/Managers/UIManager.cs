using UnityEngine;

namespace Unknown.Samuele
{
    public class UIManager : MonoBehaviour
    {
        [Header("Ticker")]
        [SerializeField] private float tickTime = 0.2f;

        private float timer = 0f;
        private bool gameIsPaused = false;

        private GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void OnEnable()
        {
            gameManager = GameManager.Instance;

            gameManager.OnPauseEvent += Pause;
            gameManager.OnResumeEvent += Resume;
        }

        void OnDisable()
        {
            gameManager.OnPauseEvent -= Pause;
            gameManager.OnResumeEvent -= Resume;
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= tickTime)
            {
                timer = 0f;

                if (gameIsPaused)
                    CheckForMouseUI();
            }
        }

        private void CheckForMouseUI()
        {
            if (gameManager.CurrentDevice == CurrentDevice.Keyboard_Mouse)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void Pause()
        {
            gameIsPaused = true;
        }

        private void Resume()
        {
            gameIsPaused = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
