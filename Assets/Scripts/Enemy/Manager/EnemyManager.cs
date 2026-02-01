using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Config Enemy Spawn")]
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform enemySpawn;
    [SerializeField] private int indexEnemy;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }

    private bool CheckAllEnemy()
    {
        bool isClear = false;
        
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            Enemy enemyComponent = enemyPrefabs[i].GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                Enemy data = enemyComponent;
                Debug.Log($"{data.enemyData.enemyName} is killed : {data.enemyData.isElimination}");
            }
        }

        return isClear;
    }

    public void SpawnEnemyGoon()
    {
        /*
         1. Spawn enemy on current Index
         2. index++
         */

        //Check there are enemy in enemySpawn

        if (indexEnemy > enemyPrefabs.Count || CheckAllEnemy())
        {
            //call GM the game is DONE now FIGHT THE BOSS!!!
            return;
        }

        GameObject newEnemy = Instantiate(enemyPrefabs[indexEnemy], enemySpawn);

        indexEnemy++;
    }
}
