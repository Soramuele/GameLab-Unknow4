using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

namespace Unknown.Samuele
{
    public class AudioManager : MonoBehaviour, IManager
    {
        public static AudioManager Instance { get; private set; }

        [Header("Mixer")]
        [SerializeField] private AudioMixer mixer;

        [Header("Groups")]
        [SerializeField] private AudioMixerGroup musicGroup;
        [SerializeField] private AudioMixerGroup sfxGroup;
        [SerializeField] private AudioMixerGroup uiGroup;

        private List<AudioSource> sfxPool = new List<AudioSource>();
        private GameObject sfxPoolParent;

        void Awake()
        {
            if (Instance == null)
                Instance = this;        
        }

        void Start()
        {
            sfxPoolParent = new GameObject("SFX Pool Parent");
        }

#region Public functions
#region Music
        public void PlayAudio(AudioClip clip, AudioSource source, float fadeDuration = .5f)
        {
            source.outputAudioMixerGroup = musicGroup;

            if (source.clip != null && source.isPlaying)
            {
                // Fade out music, then start new one
                FadeOut(source, fadeDuration, () => FadeIn(clip, source, fadeDuration));
            }
            else
            {
                // Start new music
                FadeIn(clip, source, fadeDuration);
            }
        }
#endregion Music

#region SoundFX
        public void PlaySFX(AudioClip clip, Vector3 position)
        {
            // Check for available AudioSource in pool, if not create one
            // Play SFX clip at position
            // Deactivate after 
        }
#endregion SoundFX

#region UI
        public void PlayUISound(AudioClip clip)
        {
            // Play 2D audio for UI
        }
#endregion UI
#endregion Public functions

#region Utilities
        private AudioSource RandomizePitch(AudioSource source)
        {
            source.pitch = Random.Range(0.8f, 1.2f);
            return source;
        }

        private void FadeIn(AudioClip clip, AudioSource source, float fadeDuration)
        {
            DOTween.Kill(source);
            mixer.GetFloat("MusicVolume", out var endVolume);
            endVolume = Mathf.Pow(10, endVolume / 20);

            source.clip = clip;
            source.volume = 0;
            source.Play();

            source.DOFade(endVolume, fadeDuration)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .SetId(source);
        }

        private void FadeOut(AudioSource source, float fadeDuration, TweenCallback onComplete = null)
        {
            DOTween.Kill(source);
            var currentVolume = source.volume;

            source.DOFade(0f, fadeDuration)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    source.Stop();
                    source.volume = currentVolume;
                    onComplete?.Invoke();
                })
                .SetId(source);
        }
#endregion Utilities

#region Settings
        public void SetMasterVolume(float level) =>
            mixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20);
        public float GetMasterVolume()
        {
            mixer.GetFloat("MasterVolume", out float masterVolume);
            return Mathf.Pow(10, masterVolume / 20);
        }
        
        public void SetMusicVolume(float level) =>
            mixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20);
        public float GetMusicVolume()
        {
            mixer.GetFloat("MusicVolume", out float masterVolume);
            return Mathf.Pow(10, masterVolume / 20);
        }
        
        public void SetSoundFXVolume(float level) =>
            mixer.SetFloat("SoundFXVolume", Mathf.Log10(level) * 20);
        public float GetSoundFXVolume()
        {
            mixer.GetFloat("SoundFXVolume", out float masterVolume);
            return Mathf.Pow(10, masterVolume / 20);
        }

#region Manager functions
        public void Save()
        {
            mixer.GetFloat("MasterVolume", out float masterVolume);
            mixer.GetFloat("MusicVolume", out float musicVolume);
            mixer.GetFloat("SoundFXVolume", out float soundFXVolume);

            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetFloat("SoundFXVolume", soundFXVolume);
        }

        public void Load()
        {
            var masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0);
            var musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);
            var soundFXVolume = PlayerPrefs.GetFloat("SoundFXVolume", 0);

            mixer.SetFloat("MasterVolume", masterVolume);
            mixer.SetFloat("MusicVolume", musicVolume);
            mixer.SetFloat("SoundFXVolume", soundFXVolume);
        }
#endregion Manager functions
#endregion Settings
    }
}
