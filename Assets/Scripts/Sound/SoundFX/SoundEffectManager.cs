    using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;

    [Header("Sound Effect")]
    [SerializeField] private SoundLibrary _soundEffects;
    [SerializeField] private AudioSource _soundEffectSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); ;
        }
        else
            Destroy(gameObject);
    }

    public void PlaySoundEffectOneClip(string soundEffectName)
    {
        _soundEffectSource.PlayOneShot(_soundEffects.GetClipSoundEffectByGroupID(soundEffectName));
    }

}
