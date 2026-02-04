using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("Inventory Data")]
    public List<CardSO> itemCards;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        TakeOutCard();
    }

    public void AddCardToInventory(List<CardSO> cards)
    {
        foreach(CardSO card in cards)
            itemCards.Add(card);
        
        TakeOutCard();
    }

    public void RemoveCard(CardSO cardData)
    {
        for (int i = 0; i < itemCards.Count; i++)
        {
            if (itemCards[i].name == cardData.name)
            {
                itemCards.RemoveAt(i);
                break;
            }
        }
    }

    public void TakeOutCard()
    {
        foreach (CardSO card in itemCards)
        {
            if (card.cardType == CardType.Item)
                PlayerDeckManager.instance.AddNewCard(card);
        }
    }
}
