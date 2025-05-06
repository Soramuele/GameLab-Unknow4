using UnityEngine;
using UnityEngine.Events;

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

        public UnityAction<ActiveDevice> ChangeDeviceEvent;

        void Awake()
        {
            if (Instance == null)
                Instance = this;

            // Randomize Random seed
            Random.InitState(System.DateTime.Today.Millisecond);
        }

        void Start()
        {
            // Lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void OnEnable()
        {
            inputs.ChangeDeviceEvent += ctx => ChangeDeviceEvent?.Invoke(ctx);
        }

        void OnDisable()
        {
            inputs.ChangeDeviceEvent -= ctx => ChangeDeviceEvent?.Invoke(ctx);
        }
    }
}
