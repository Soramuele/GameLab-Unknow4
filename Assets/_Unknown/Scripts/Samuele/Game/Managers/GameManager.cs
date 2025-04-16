using UnityEngine;

namespace Unknown.Samuele
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        // Current controls
        [Header("Inputs")]
        [SerializeField] private InputHandler inputs;

        private GameplayManager gameplayManager = new GameplayManager();
        public GameplayManager GetGameplay() =>
            gameplayManager;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        void Start()
        {
            // Lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            // Check for active device
            if (inputs.currentControlScheme == "Controller")
                gameplayManager.ChangeActiveDevice(GameplayManager.ActiveDevice.Controller);
            else
                gameplayManager.ChangeActiveDevice(GameplayManager.ActiveDevice.Keyboard);
        }
    }
}
