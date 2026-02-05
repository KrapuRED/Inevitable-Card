using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public class GachaPool
{
    [Range(0, 100)] public int weight;
    public CardSO cardData;
}

public class GachaManager : MonoBehaviour
{
    public static GachaManager instance;

    [Header("Gacha Config")]
    public List<GachaPool> pools;
    private List<CardSO> obtainCard;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public void GachaCard(EnemyStatus enemyStatus)
    {
        obtainCard = new List<CardSO>();

        int dropCount = enemyStatus == EnemyStatus.Strong ? 6 : 3;

        Debug.Log("drop count : " + dropCount);

        // Calculate total weight ONCE
        int totalWeights = 0;
        for (int i = 0; i < pools.Count; i++)
        {
            totalWeights += pools[i].weight;
        }

        if (totalWeights > 100)
        {
            Debug.LogWarning("Total weights exceed 100!");
            return;
        }

        // Roll N times
        for (int rollIndex = 0; rollIndex < dropCount; rollIndex++)
        {
            int roll = Random.Range(0, totalWeights);
            int current = 0;

            for (int i = 0; i < dropCount; i++)
            {
                current += pools[i].weight;
                if (roll < current)
                {
                    obtainCard.Add(pools[i].cardData);
                    break;
                }
            }
        }

        HUDManager.instance.UpdateObtainCard(obtainCard);
        InventoryManager.instance.AddCardToInventory(obtainCard);

        DebugObtainCards();
    }


    private void DebugObtainCards()
    {
        foreach(CardSO card in obtainCard)
            Debug.Log($"Get Card : {card.cardName}");
    }
}
