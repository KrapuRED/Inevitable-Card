using UnityEngine;
using System.Collections.Generic;
using System;

public class DeckContiner : MonoBehaviour
{
    [Header("Config Card Deck")]
    public CardType cardType;
    [SerializeField] private int maxCardDeck;
    // Item cards use count
    private Dictionary<CardSO, int> itemCards = new();
    // Non-item cards use list
    private List<CardSO> movementCards = new();

    [Header("Events")]
    public OnAddCardEventSO OnAddCardEvent;
    public OnRemoveCardEventSO OnRemoveCardEvent;

    //private List<CardSO> cards = new List<CardSO>();

    public void AddCard(CardSO newCardName)
    {
        /*
        Debug.Log($"AddCard called on {name}");
        Debug.Log($"DeckType: {cardType}, CardType: {newCardName.cardType}");
        Debug.Log($"Current: {cards.Count}, Max: {maxCardDeck}");
        */

        if (newCardName.cardType == CardType.Item)
        {
            if (!itemCards.ContainsKey(newCardName))
                itemCards[newCardName] = 0;

            itemCards[newCardName]++;
        }
        else
        {
            if (movementCards.Contains(newCardName))
                return;

            movementCards.Add(newCardName);
        }

            OnAddCardEvent.Raise(newCardName);
    }
    
    public void ReturnCard(CardSO cardSO)
    {
        AddCard(cardSO);
    }

    public void RemoveCardFromDeck(CardSO removeCardName)
    {
        if (newCardName.cardType == CardType.Item)
        {
            itemCards.Remove(removeCardName);
        }
        else
        {

        }
            cards.Remove(removeCardName);
        OnRemoveCardEvent.Raise(removeCardName);
    }

    public int Count => cards.Count;
}
