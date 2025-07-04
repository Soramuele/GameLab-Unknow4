using UnityEngine;

namespace Unknown.Samuele
{
    public class UIManager : MonoBehaviour
    {
        [Header("Ticker")]
        [SerializeField] private float tickTime = 0.2f;

        private float timer = 0f;

        private GameManager gameManager;

        void Start()
        {
            gameManager = GameManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= tickTime)
            {
                timer = 0f;

                CheckForMouseUI();
            }
        }

        private void CheckForMouseUI()
        {
            if (gameManager.CurrentInputMap != InputMap.UI)
            {
                HideCursor();
                return;
            }

            if (gameManager.CurrentDevice == CurrentDevice.Keyboard_Mouse)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                HideCursor();
            }
        }

        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
