using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string nextScene;
    public string transitoinScene;

    public void PlayGame()
    {
        TransitionManager.instance.LoadSceneTransition(nextScene, transitoinScene);
    }

    public void OpenCredit()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
