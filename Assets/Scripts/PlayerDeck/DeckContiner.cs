using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class CardInstance
{
    public int ID;
    public CardSO cardData;

    public CardInstance(CardSO cardData, int id)
    {
        this.cardData = cardData;
        this.ID = id;
    }
}

public class DeckContiner : MonoBehaviour
{
    [Header("Config Card Deck")]
    public CardType cardType;

    [Header("Events")]
    public OnAddCardEventSO OnAddCardEvent;
    public OnRemoveCardEventSO OnRemoveCardEvent;
    public OnRepositioningCardEventSO OnRepositioningCardEvent;

    //private List<CardSO> cards = new List<CardSO>();
    public virtual void AddCard(CardSO newCardName)
    {
       
    }
    
    public virtual void ReturnCard(CardSO cardSO)
    {
        
    }

    public virtual void RemoveCardFromDeck(CardInstance removeCardName)
    {
        
    }
}
