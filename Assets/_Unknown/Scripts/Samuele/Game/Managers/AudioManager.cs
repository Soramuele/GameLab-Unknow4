using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

namespace Unknown.Samuele
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Mixer")]
        [SerializeField] private AudioMixer mixer;

        [Header("Sources")]
        [SerializeField] private AudioSource uiSource;
        [SerializeField] private AudioSource ambientSource;

        [Header("SFX Prefab")]
        [SerializeField] private AudioSource sfxSource;

        private List<AudioSource> sfxPool = new();
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

        #region Functions
        public void PlayAudio(AudioClip clip, AudioSource source, float fadeDuration = .5f)
        {
            if (source.isPlaying)
                Fade(clip, source, fadeDuration);
            else
                FadeIn(clip, source, fadeDuration);

            source.loop = true;
        }

        public void StopAudio(AudioSource source)
        {
            FadeOut(source);

            source.loop = false;
        }

        public void PlaySFX(AudioClip clip, Vector3 position)
        {
            AudioSource availableSfx = null;
            foreach (var sfx in sfxPool)
                if (!sfx.isPlaying)
                {
                    availableSfx = sfx;
                    break;
                }

            if (availableSfx == null)
            {
                availableSfx = Instantiate(sfxSource, sfxPoolParent.transform);
                sfxPool.Add(availableSfx);
            }

            availableSfx.transform.position = position;
            availableSfx.clip = clip;
            availableSfx.Play();
        }

        public void PlayFootsteps(AudioClip clip, AudioSource source)
        {
            source.loop = false;
            source.clip = clip;
            RandomizePitch(source);
            source.Play();
        }

        public void PlayUISound()
        {
            uiSource.Play();
        }
#endregion Functions

#region Utilities
        private void RandomizePitch(AudioSource source) =>
            source.pitch = Random.Range(0.85f, 1.15f);

        private void Fade(AudioClip clip, AudioSource source, float fadeDuration)
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
                    FadeIn(clip, source, fadeDuration);
                })
                .SetId(source);
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

        private void FadeOut(AudioSource source, float fadeDuration = 0.5f)
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
        void Save()
        {
            mixer.GetFloat("MasterVolume", out float masterVolume);
            mixer.GetFloat("MusicVolume", out float musicVolume);
            mixer.GetFloat("SoundFXVolume", out float soundFXVolume);

            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetFloat("SoundFXVolume", soundFXVolume);
        }

        void Load()
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
