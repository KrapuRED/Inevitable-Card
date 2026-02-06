using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;

    public GameObject transitionContainer;
    public Slider progressBar;
    [SerializeField] private SceneTransition[] transitions;

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

    private void Start()
    {
        if (transitionContainer != null)
            transitions = transitionContainer.GetComponentsInChildren<SceneTransition>(true);
    }

    public void LoadSceneTransition(string sceneName, string transitionName = "CrossFade")
    {
        StartCoroutine(LoadScenbeAsync(sceneName, transitionName));
    }

    private IEnumerator LoadScenbeAsync(string sceneName, string transitionName)
    {
        SceneTransition transition = transitions.First(t => t.name == transitionName);

        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        yield return transition.AnimationTransitionIn();

        progressBar.gameObject.SetActive(true);

        do
        {
            progressBar.value = scene.progress;
            yield return null;
        } while (scene.progress < 0.9f);

        yield return new WaitForSeconds(1.5f);

        scene.allowSceneActivation = true;

        progressBar.gameObject.SetActive(false);

        yield return transition.AnimationTransitionOut();
    }
}
