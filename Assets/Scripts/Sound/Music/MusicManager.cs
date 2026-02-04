using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Config Music")]
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Reference")]
    [SerializeField] private MusicLibrary _musicLibabry;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void PlayMusicBackground(string trackName)
    {
        StartCoroutine(AnimateMusicCrossFade(_musicLibabry.FindAudioClipMusicByName(trackName), fadeDuration));
    }

    IEnumerator AnimateMusicCrossFade(AudioClip musicAudioClip, float fadeDuration)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;
        }

        musicSource.clip = musicAudioClip;
        musicSource.Play();

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;
        }
    }
}
