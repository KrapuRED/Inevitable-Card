using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        EnemyManager.instance.SpawnEnemyGoon();
    }

    public void FightBoss()
    {
        Debug.Log("We kill all the Goons! NOW WE FIGHT THE BOSS!");
    }
}
