using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "SOAudio", menuName = "ScriptableObjects/Audios")]
public class SOAudio : ScriptableObject
{
    [Header("Music")]
    [SerializeField] private SerializedDictionary<string, List<AudioClip>> musicClips;

    [Header("SoundFX")]
    [SerializeField] private SerializedDictionary<string, List<AudioClip>> sfxClips;

    public SerializedDictionary<string, List<AudioClip>> MusicClips => musicClips;
    public SerializedDictionary<string, List<AudioClip>> SFXClips => sfxClips;
}
