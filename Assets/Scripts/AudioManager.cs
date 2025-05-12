using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Unknown.Samuele
{
    public class AudioManager : MonoBehaviour, IManager
    {
        public static AudioManager Instance { get; private set; }

        [Header("Mixer & Groups")]
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioMixerGroup musicMixerGroup;
        [SerializeField] private AudioMixerGroup soundFXMixerGroup;

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
        public void PlayAudio(AudioClip clip, AudioSource source, float fadeDuration = 1)
        {
            source.outputAudioMixerGroup = musicMixerGroup;

            if (source.clip != null)
            {
                // Fade out music, then start new one
                StartCoroutine(FadeMusic(clip, source, fadeDuration));
            }
            else
            {
                // Start new music
                source.clip = clip;
                source.Play();
            }
        }

        public void PlayAudio(AudioClip clip, GameObject customSource, float fadeDuration = 1)
        {
            // Find existing source in customSource or create new one as child
            var source = GetOrCreateNewAudioSource(customSource, musicMixerGroup);

            PlayAudio(clip, source, fadeDuration);
        }

        public void PlayAudio(AudioClip[] clip, AudioSource source, float fadeDuration = 1)
        {
            var myClip = GetRandomClip(clip);

            PlayAudio(myClip, source, fadeDuration);
        }

        public void PlayAudio(AudioClip[] clip, GameObject customSource, float fadeDuration = 1)
        {
            // Find existing source in customSource or create new one ac child
            var source = GetOrCreateNewAudioSource(customSource, musicMixerGroup);

            var myClip = GetRandomClip(clip);

            PlayAudio(myClip, source, fadeDuration);
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
    #endregion Public functions

    #region Utilities
        private AudioClip GetRandomClip(AudioClip[] clip) =>
            clip[Random.Range(0, clip.Length - 1)];

        private AudioSource RandomizePitch(AudioSource source)
        {
            source.pitch = Random.Range(0.8f, 1.2f);
            return source;
        }

        private AudioSource GetOrCreateNewAudioSource(GameObject customSource, AudioMixerGroup mixer)
        {
            var source = customSource.GetComponent<AudioSource>();
            
            if (source == null)
                source = customSource.GetComponentInChildren<AudioSource>();
            if (source == null)
                source = new GameObject("AudioSource").AddComponent<AudioSource>();
            
            source.outputAudioMixerGroup = mixer;
            Instantiate(source, customSource.transform);
            
            return source;
        }

        private IEnumerator FadeMusic(AudioClip clip, AudioSource source, float fadeDuration)
        {
            float startVolume = source.volume;

            // Fade out
            for (float i = 0; i < fadeDuration; i += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(startVolume, 0, i / fadeDuration);
                yield return null;
            }

            source.clip = clip;
            source.Play();

            // Fade in
            for (float i = 0; i < fadeDuration; i += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(0, startVolume, i / fadeDuration);
                yield return null;
            }
        }
    #endregion Utilities

    #region Settings
        public void SetMasterVolume(float level) =>
            mixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20);
        
        public void SetMusicVolume(float level) =>
            mixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20);
        
        public void SetSoundFXVolume(float level) =>
            mixer.SetFloat("SoundFXVolume", Mathf.Log10(level) * 20);

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

            mixer.SetFloat("MasterVolume", Mathf.Pow(10, masterVolume / 20));
            mixer.SetFloat("MusicVolume", Mathf.Pow(10, musicVolume / 20));
            mixer.SetFloat("SoundFXVolume", Mathf.Pow(10, soundFXVolume / 20));
        }
    #endregion Manager functions
    #endregion Settings
    }
}
