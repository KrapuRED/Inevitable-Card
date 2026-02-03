using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyPickCard : MonoBehaviour
{
    private Enemy _currentEnemy;

    [Header("Card list")]
    public List<CardSO> cards = new List<CardSO>();
    private List<CardInstance> instanceList = new List<CardInstance>();
    private int index;

    private void Awake()
    {
        _currentEnemy = GetComponentInParent<Enemy>();
    }

    public List<CardInstance> GetEnemyCardList(int maxSlots)
    {
        if (_currentEnemy == null)
        {
            Debug.LogWarning($"{this.name} dont have Enemy");
            return null;
        }

        index = 0;
        instanceList.Clear();
        for (int i = 0; i < maxSlots; i++)
        {
            CardInstance card = new CardInstance(RandomPickCard(), index++);
            instanceList.Add(card);
        }

        return instanceList;
    }

    public List<CardInstance> GetEnemyCardListSequential(int maxSlots)
    {
        index = 0;
        instanceList.Clear();

        for (int i = 0; i < maxSlots; i++)
        {
            CardSO cardData = cards[Mathf.Min(i, cards.Count - 1)];
            instanceList.Add(new CardInstance(cardData, index++));
        }

        return instanceList;
    }

    public void SetCardDeckPool(List<CardSO> cardDatas)
    {
        cards.Clear();
        cards.AddRange(cardDatas);
    }

    private CardSO RandomPickCard()
    {
        if (cards.Count < 0)
        {
            Debug.LogError("Enemy card list is empty!");
            return null;
        }

        int randomIndex = Random.Range(0, cards.Count);

        CardSO card = cards[randomIndex];
        return card;
    }

    IEnumerator DelayDebug()
    {
        GetEnemyCardList(5);
        Debug.Log("Enemy Picking Cards");
        yield return new WaitForSeconds(1.5f);
        foreach (CardInstance card in instanceList)
        {
            Debug.Log($"Card name {card.cardData.cardName}  Card ID : {card.ID + 1}");
        }
    }
}
