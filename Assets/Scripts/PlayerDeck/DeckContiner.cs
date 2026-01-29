using UnityEngine;
using System.Collections.Generic;
using System;

public class DeckContiner : MonoBehaviour
{
    [Header("Config Card Deck")]
    public CardType cardType;
    [SerializeField] private int maxCardDeck;

    [Header("Events")]
    public OnAddCardEventSO OnAddCardEvent;
    public OnRemoveCardEventSO OnRemoveCardEvent;

    private List<CardSO> cards = new List<CardSO>();

    public void AddCard(CardSO newCardName)
    {
        /*Debug.Log($"AddCard called on {name}");
        Debug.Log($"DeckType: {cardType}, CardType: {newCardName.cardType}");
        Debug.Log($"Current: {cards.Count}, Max: {maxCardDeck}");*/

        if (newCardName.cardType != CardType.Item)
        {
            cards.Add(newCardName);
            OnAddCardEvent.Raise(newCardName);
        }
    }

    public void RemoveCardFromDeck(CardSO removeCardName)
    {
        cards.Remove(removeCardName);
        OnRemoveCardEvent.Raise(removeCardName);
    }

    public int Count => cards.Count;
}
