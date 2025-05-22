using System.Collections;
using TMPro;
using UnityEngine;

namespace Unknown.Samuele
{
    public class FloppyManager : MonoBehaviour
    {
        public static FloppyManager Instance { get; private set; }

        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;

        [Header("Player")]
        [SerializeField] private FloppyPlayer player;

        [Header("Screens")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text playText;
        [SerializeField] private GameObject getReady;
        [SerializeField] private GameObject gameOver;
        [SerializeField] private TMP_Text recordText;

        private int score = 0;
        private int record = 0;

        private PipeSpawner pipeSpawner;

        public int Score => score;

        void Awake()
        {
            Instance = this;

            pipeSpawner = GetComponentInChildren<PipeSpawner>();
        }

        public void Start()
        {
            record = PlayerPrefs.GetInt("FloppyBurdScore", 0);
            recordText.text = $"Your record = {record}";

            gameOver.SetActive(false);
            recordText.gameObject.SetActive(false);

            playText.gameObject.SetActive(true);

            Pause();

            Pipes[] pipes = FindObjectsOfType<Pipes>();

            foreach (var pipe in pipes)
                Destroy(pipe.gameObject);
        }

        void OnEnable()
        {
            GameManager.Instance.OnChangeDeviceEvent += ChangeDeviceForPlay;
            inputHandler.OnBackEvent += BackToMainGame;
            inputHandler.OnJumpEvent += () => Play();

        }

        void OnDisable()
        {
            GameManager.Instance.OnChangeDeviceEvent -= ChangeDeviceForPlay;
            inputHandler.OnBackEvent -= BackToMainGame;
            inputHandler.OnJumpEvent -= () => Play();
        }

        public void Play()
        {
            score = 0;
            scoreText.text = score.ToString();

            getReady.SetActive(false);
            gameOver.SetActive(false);
            playText.gameObject.SetActive(false);

            Resume();

            Pipes[] pipes = FindObjectsOfType<Pipes>();

            foreach (var pipe in pipes)
                Destroy(pipe.gameObject);
        }

        public void GameOver()
        {
            if (score > record)
                record = score;

            gameOver.SetActive(true);
            recordText.gameObject.SetActive(true);
            recordText.text = $"Record = {record}";

            playText.gameObject.SetActive(true);

            Pause();
        }

        private void Pause()
        {
            player.Pause();

            pipeSpawner.Pause();
        }

        private void Resume()
        {
            player.Resume();

            pipeSpawner.Resume();
        }

        public void IncreaseScore()
        {
            score++;
            scoreText.text = score.ToString();
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

        void OnDestroy()
        {
            PlayerPrefs.SetInt("FloppyBurdScore", record);
        }

        private void BackToMainGame()
        {
            GameOver();

            GameManager.Instance.ChangeInputMap(GameManager.InputMap.None);
            CameraManager.Instance.SwitchToMainCamera();

            StartCoroutine(WaitForBlend());
        }
        
        private IEnumerator WaitForBlend()
        {
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
