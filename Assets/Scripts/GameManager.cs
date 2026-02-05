using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Status Game")]
    [SerializeField] private int currentBattle;
    [SerializeField] private string defaultBGM;
    [SerializeField] private string bossBGM;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        MusicManager.instance.PlayMusicBackground(defaultBGM);
        EnemyManager.instance.SpawnEnemyGoon(currentBattle);  
        //FightBoss();
    }

    public void NextBattle()
    {
        currentBattle++;
        HUDManager.instance.UpdateBattleStageUI(currentBattle - 1);
        if (currentBattle >= 5)
        {
            FightBoss();
        }
        else
        {
            EnemyManager.instance.SpawnEnemyGoon(currentBattle);
        }
    }

    public void FightBoss()
    {
        Debug.Log("We kill all the Goons! NOW WE FIGHT THE BOSS!");
        MusicManager.instance.PlayMusicBackground(bossBGM);
        EnemyManager.instance.SpawnEnemyBoss();
    }

    public void PlayerWin()
    {
        HUDManager.instance.OpenPanel(PanelName.WinningPanel);
    }

    public void PlayerLose()
    {
        HUDManager.instance.OpenPanel(PanelName.GameOverPanel);
    }

    public void RestartGame()
    {
        SceneManagement.instance.ChangeScene("StartScene");
    }

    public void BackToMainMenu()
    {
        SceneManagement.instance.ChangeScene("MainMenu");
    }
}
