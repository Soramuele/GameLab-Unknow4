using UnityEngine;
using UnityEngine.UI;

namespace Unknown.Samuele
{
    public class InGameMenuManager : MonoBehaviour
    {
        [Header("Parent Menu")]
        [SerializeField] private GameObject menu;

        [Header("Buttons")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;

        // [Header("Sliders")]
        // [SerializeField] private Slider masterVolumeSlider;
        // [SerializeField] private Slider musicVolumeSlider;
        // [SerializeField] private Slider sfxVolumeSlider;

        private GameManager gameManager;
        private AudioManager audioManager;

        void Awake()
        {
            gameManager = GameManager.Instance;
        }

        // Start is called before the first frame update
        void Start()
        {
            audioManager = AudioManager.Instance;

            // Setup sliders
            // InitiateSliders();

            // Add listeners to UI elements
            resumeButton.onClick.AddListener(ButtonResumeGame);
            quitButton.onClick.AddListener(QuitGame);
            
            // masterVolumeSlider.onValueChanged.AddListener(ctx => audioManager.SetMasterVolume(ctx));
            // musicVolumeSlider.onValueChanged.AddListener(ctx => audioManager.SetMusicVolume(ctx));
            // sfxVolumeSlider.onValueChanged.AddListener(ctx => audioManager.SetSoundFXVolume(ctx));

            menu.SetActive(false);
        }

        void OnEnable()
        {
            gameManager.OnPauseEvent += PauseGame;
            gameManager.OnResumeEvent += ResumeGame;
        }

        void OnDisable()
        {
            gameManager.OnPauseEvent -= PauseGame;
            gameManager.OnResumeEvent -= ResumeGame;
        }

        public void ChangeMasterVolume(float value)
        {
            audioManager.SetMasterVolume(value);
        }

        public void ChangeMusicVolume(float value)
        {
            audioManager.SetMusicVolume(value);
        }

        public void ChangeSoundFXVolume(float value)
        {
            audioManager.SetSoundFXVolume(value);
        }

        // private void InitiateSliders()
        // {
        //     masterVolumeSlider.value = audioManager.GetMasterVolume();
        //     musicVolumeSlider.value = audioManager.GetMusicVolume();
        //     sfxVolumeSlider.value = audioManager.GetSoundFXVolume();
        // }

        private void PauseGame()
        {
            Debug.Log("Dio cane");
            menu.SetActive(true);
        }

        private void ResumeGame()
        {
            menu.SetActive(false);
        }

        private void ButtonResumeGame()
        {
            GameManager.Instance.ResumeGameFromSettings();
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}
