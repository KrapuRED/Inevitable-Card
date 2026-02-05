using UnityEngine;
using System.Collections.Generic;

public class GachaManager : MonoBehaviour
{
    public static GachaManager instance;

    [Header("Gacha Config")]
    public List<CardSO> cards;
    public List<int> weights;
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

        // Calculate total weight ONCE
        int totalWeights = 0;
        for (int i = 0; i < dropCount; i++)
        {
            totalWeights += weights[i];
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
                current += weights[i];
                if (roll < current)
                {
                    obtainCard.Add(cards[i]);
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
