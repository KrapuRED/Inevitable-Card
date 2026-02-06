using UnityEngine;
using UnityEngine.Playables;

public class CheckDurationCutScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    public string nextScene;
    public string transitionName;

    private void OnEnable()
    {
        if (_director != null)
            _director.stopped += CutSceneEnd;
    }

    private void OnDisable()
    {
        if (_director != null)
            _director.stopped -= CutSceneEnd;
    }

    private void CutSceneEnd(PlayableDirector director)
    {
        Debug.Log("Cutscene Ended!");
        TransitionManager.instance.LoadSceneTransition(nextScene, transitionName);
    }
}
