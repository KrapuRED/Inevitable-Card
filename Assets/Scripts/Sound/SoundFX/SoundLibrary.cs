using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SoundEffect
{
    public string groupID;
    public AudioClip[] clipSoundEffects;
}

public class SoundLibrary : MonoBehaviour
{
    public SoundEffect[] soundEffects;   

    public AudioClip GetClipSoundEffectByGroupID(string groupName)
    {
        foreach (var soundEffect in soundEffects)
        {
            if (soundEffect.groupID == groupName)
            {
                return soundEffect.clipSoundEffects[Random.Range(0, soundEffect.clipSoundEffects.Length)];
            }
        }
        Debug.LogWarning($"There are no sound effect with name {groupName}");
        return null;
    }
}
