using System.Collections.Generic;
using UnityEngine;

public class ItemDeckContainer : DeckContiner
{
    [Header("Config Item Deck Container")]
    [SerializeField] private int maxCardDeck;
    private int nextID;
    private List<CardInstance> cards = new();

    [Header("Events")]
    public OnHideItemCardEventSO OnHideItemCardEvent;

    public override void AddCard(CardSO newCardName)
    {

        /*Debug.Log($"AddCard called on {name}");
        Debug.Log($"DeckType: {cardType}, CardType: {newCardName.cardType}");
        Debug.Log($"Current: {cards.Count}, Max: {maxCardDeck}");*/
        if (cards.Count >= maxCardDeck)
            return;

        CardInstance newCard = new CardInstance(newCardName, nextID++);
        cards.Add(newCard);

        /* Debug Cards
        foreach (var card in cards)
        {
            Debug.Log($"Cards ID : {card.ID} Card Name : {card.cardData.cardName}");
        }*/

        OnAddCardEvent.Raise(newCard);
    }

    public override void ReturnCard(CardSO cardSO)
    {
        AddCard(cardSO);
    }

    public void HideItemCard(CardInstance card)
    {
        OnHideItemCardEvent?.onHide(card);
    }

    public override void RemoveCardFromDeck(CardInstance removeCardName)
    {
        cards.Remove(removeCardName);
        OnRemoveCardEvent.Raise(removeCardName);
    }
}
