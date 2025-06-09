using System;
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

        [Header("Sliders")]
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;

        private GameManager gameManager;
        private AudioManager audioManager;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameManager.Instance;
            audioManager = AudioManager.Instance;

            // Setup sliders
            InitiateSliders();

            // Add listeners to UI elements
            resumeButton.onClick.AddListener(ButtonResumeGame);
            quitButton.onClick.AddListener(QuitGame);
            masterVolumeSlider.onValueChanged.AddListener(ctx => audioManager.SetMasterVolume(ctx));
            musicVolumeSlider.onValueChanged.AddListener(ctx => audioManager.SetMusicVolume(ctx));
            sfxVolumeSlider.onValueChanged.AddListener(ctx => audioManager.SetSoundFXVolume(ctx));

            ResumeGame();
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

        private void InitiateSliders()
        {
            masterVolumeSlider.value = audioManager.GetMasterVolume();
            musicVolumeSlider.value = audioManager.GetMusicVolume();
            sfxVolumeSlider.value = audioManager.GetSoundFXVolume();
        }

        private void PauseGame()
        {
            menu.SetActive(true);
        }

        private void ResumeGame()
        {
            menu.SetActive(false);
        }

        private void ButtonResumeGame()
        {
            gameManager.OnResumeEvent?.Invoke();
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}
