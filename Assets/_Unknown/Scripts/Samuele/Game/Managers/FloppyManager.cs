using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Unknown.Samuele
{
    public class FloppyManager : MonoBehaviour
    {
        public static FloppyManager Instance { get; private set; }

        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;

        [Header("Player")]
        [SerializeField] private FloppyPlayer player;
        private Vector3 playerStartingPosition;

        [Header("Play")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private GameObject getReady;
        [SerializeField] private TMP_Text playText;

        [Header("Game Over")]
        [SerializeField] private GameObject gameOver;
        private TMP_Text recordText;

        private int score = 0;
        private int record = 0;
        private bool isPlaying = false;

        private PipeSpawner pipeSpawner;

        public UnityAction OnCloseEvent;

        void Awake()
        {
            Instance = this;

            pipeSpawner = GetComponentInChildren<PipeSpawner>();
        }

        void Start()
        {
            record = PlayerPrefs.GetInt("FloppyBurdScore", 0);

            recordText = gameOver.GetComponentInChildren<TMP_Text>();
            gameOver.SetActive(false);

            playText.gameObject.SetActive(true);

            playerStartingPosition = player.transform.position;
        }

        void OnEnable()
        {
            GameManager.Instance.OnChangeDeviceEvent += ChangeDeviceForPlay;
            inputHandler.OnBackEvent += BackToMainGame;
            inputHandler.OnJumpEvent += Play;
        }

        void OnDisable()
        {
            GameManager.Instance.OnChangeDeviceEvent -= ChangeDeviceForPlay;
            inputHandler.OnBackEvent -= BackToMainGame;
            inputHandler.OnJumpEvent -= Play;
        }

        private void Play()
        {
            if (isPlaying)
                return;
            else
                isPlaying = true;

            StartGame();

            score = 0;
            scoreText.text = score.ToString();

            getReady.SetActive(false);
            gameOver.SetActive(false);
            playText.gameObject.SetActive(false);

            player.transform.position = playerStartingPosition;
        }

        public void GameOver()
        {
            if (score > record)
            {
                record = score;
                PlayerPrefs.SetInt("FloppyBurdScore", record);
            }

            gameOver.SetActive(true);
            recordText.text = $"Record = {record}";

            playText.gameObject.SetActive(true);

            isPlaying = false;
            Pause();
        }

        public void IncreaseScore()
        {
            score++;
            scoreText.text = score.ToString();
        }

        private void Pause()
        {
            player.CanPlay = false;
            pipeSpawner.Pause();

        }

        private void StartGame()
        {
            player.CanPlay = true;
            pipeSpawner.StartGame();

        }

        private void ChangeDeviceForPlay(GameManager.CurrentDevice ctx)
        {
            var key = "";
            switch (ctx)
            {
                case GameManager.CurrentDevice.Keyboard_Mouse:
                    key = "space";
                    break;
                case GameManager.CurrentDevice.XBoxController:
                    key = "xa";
                    break;
                case GameManager.CurrentDevice.PlayStationController:
                    key = "px";
                    break;
                default:
                    key = "space";
                    break;
            }

            playText.text = $"Press <sprite name={key}> to play";
        }

        private void BackToMainGame()
        {
            GameOver();

            GameManager.Instance.ChangeInputMap(GameManager.InputMap.None);
            CameraManager.Instance.SwitchToMainCamera();

            Debug.LogWarning("Go back!");
            StartCoroutine(WaitForBlend());

            OnCloseEvent?.Invoke();
        }
        
        private IEnumerator WaitForBlend()
        {
            Debug.LogWarning("Coroutine started");
            yield return null;

            var gameManager = GameManager.Instance;
            var cameraManager = CameraManager.Instance;

            while (cameraManager.IsBlending)
                yield return null;

            gameManager.ShowUI();

            gameManager.ChangeInputMap(GameManager.InputMap.Gameplay);
        }
    }
}
