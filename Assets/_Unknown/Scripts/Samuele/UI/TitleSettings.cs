using UnityEngine;
using UnityEngine.UI;

namespace Unknown.Samuele
{
    public class TitleSettings : MonoBehaviour
    {
        [Header("Sliders")]
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private AudioManager audioManager;

        // Start is called before the first frame update
        void Start()
        {
            audioManager = AudioManager.Instance;

            InitializeSliders();
        }

        private void InitializeSliders()
        {
            masterSlider.value = audioManager.GetMasterVolume();
            musicSlider.value = audioManager.GetMusicVolume();
            musicSlider.value = audioManager.GetSoundFXVolume();
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
    }
}
