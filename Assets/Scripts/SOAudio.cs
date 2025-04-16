using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "SOAudio", menuName = "ScriptableObjects/Audios")]
public class SOAudio : ScriptableObject
{
    [Header("Music")]
    public SerializedDictionary<string, List<AudioClip>> musicClips;

    [Header("SoundFX")]
    public SerializedDictionary<string, List<AudioClip>> sfxClips;
}
