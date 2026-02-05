using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string nextScene;

    public void PlayGame()
    {
        SceneManagement.instance.ChangeScene(nextScene);
    }

    public void OpenCredit()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
