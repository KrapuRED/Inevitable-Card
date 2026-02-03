using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnEntry
{
    public GameObject prefab;
    [Range(0, 100)] public int weight;
}

[System.Serializable]
public class EnemyStatusConfig
{
    public EnemyStatus status;
    [Range(0, 100)] public int teirChange;
    public List<EnemySpawnEntry> enemies;
}

[System.Serializable]
public class BattleSpawnConfig
{
    public int battleIndex;
    public List<EnemyStatusConfig> tiers;
}

public static class WeightedRandom
{
    public static T GetRandom<T>(List<(T item, int weight)> items)
    {
        int totalWeight = 0;
        foreach (var i in items)
            totalWeight += i.weight;

        int roll = Random.Range(0, totalWeight);
        int current = 0;

        foreach (var i in items)
        {
            current += i.weight;
            if (roll < current)
                return i.item;
        }

        return default;
    }
}

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Config Enemy Spawn")]
    public List<BattleSpawnConfig> battleSpawnConfigs;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform enemySpawn;
    [SerializeField] private GameObject enemyBossPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }

    public void SpawnEnemyGoon(int currentBattle)
    {

        /* RULE ENEMY SPAWN
         currentBattle = 1
         Normal 100%
            Rougue Thick        = 80% 
            Defeact Treack      = 9%
            Construnction Unit  = 1%
         strong 0%
            Rouge Thick Tick    = 0%
            Menacing Track Unit = 0%
            Superviosr Unit     = 0%

         currentBattle = 2
         Normal 80%
            Rougue Thick        = 50%
            Defeact Treack      = 35%
            Construnction Unit  = 15%
         strong 20%
            Rouge Thick Tick    = 70%
            Menacing Track Unit = 20%
            Superviosr Unit     = 10%

         currentBattle = 3
         Normal
            Rougue Thick        = 40% 
            Defeact Treack      = 40%
            Construnction Unit  = 20%
         strong 40%
            Rouge Thick Tick    = 50%
            Menacing Track Unit = 35%
            Superviosr Unit     = 15

        currentBattle = 4
         Normal
            Rougue Thick        = 0% 
            Defeact Treack      = 0%
            Construnction Unit  = 0%
         strong 40%
            Rouge Thick Tick    = 10%
            Menacing Track Unit = 45%
            Superviosr Unit     = 45%
         */

        BattleSpawnConfig battle = battleSpawnConfigs.Find(b => b.battleIndex == currentBattle);

        if (battle == null)
        {
            Debug.LogError("Battle config not found!");
            return;
        }

        //roll dice
        var tierRollList = new List<(EnemyStatusConfig, int)>();
        foreach (var tier in battle.tiers)
        {
            tierRollList.Add((tier, tier.teirChange));
        }

        EnemyStatusConfig selectedTier = WeightedRandom.GetRandom(tierRollList);

        var enemRollList = new List<(GameObject, int)>();
        foreach (var enemy in selectedTier.enemies)
            enemRollList.Add((enemy.prefab, enemy.weight));

        GameObject selectedEnemy = WeightedRandom.GetRandom(enemRollList);

        Instantiate(selectedEnemy, enemySpawn);
    }

    public void SpawnEnemyBoss()
    {
        Instantiate(enemyBossPrefab, enemySpawn);
    }
}
