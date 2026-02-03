using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Status Game")]
    [SerializeField] private int currentBattle;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        EnemyManager.instance.SpawnEnemyGoon(currentBattle);  
    }

    public void NextBattle()
    {
        currentBattle++;
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
        EnemyManager.instance.SpawnEnemyBoss();
    }
}
