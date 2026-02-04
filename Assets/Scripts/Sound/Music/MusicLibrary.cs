using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public struct MusicTrack
{
    public string nameTrack;
    public AudioClip musicAudioClip;
}

public class MusicLibrary : MonoBehaviour
{
    public List<MusicTrack> musicTracks;

    public AudioClip FindAudioClipMusicByName(string musicTrack)
    {
        foreach (MusicTrack track in musicTracks)
        {
            if (track.nameTrack == musicTrack)
                return track.musicAudioClip;
        }

        return null;
    }
}
