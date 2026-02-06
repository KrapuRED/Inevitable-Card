using UnityEngine;

public class CallAudioSoundEffect : MonoBehaviour
{
    public string nameSoundEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundEffectManager.instance.PlaySoundEffectOneClip(nameSoundEffect);
    }
}
