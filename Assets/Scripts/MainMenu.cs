using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string nextScene;
    public string transitoinScene;

    private void Start()
    {
        MusicManager.instance.PlayMusicBackground("MainMenu");
    }

    public void PlayGame()
    {
        MusicManager.instance.StopMusic();
        TransitionManager.instance.LoadSceneTransition(nextScene, transitoinScene);
    }

    public void OpenCredit()
    {
        MusicManager.instance.StopMusic();
        TransitionManager.instance.LoadSceneTransition("CreditScene", transitoinScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
