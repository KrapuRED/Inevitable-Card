using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{
    public static CutSceneManager instance;

    [SerializeField] private PlayableDirector director;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayCutScene()
    {
        if (director == null)
            return;

        director.Play();
    }
}
