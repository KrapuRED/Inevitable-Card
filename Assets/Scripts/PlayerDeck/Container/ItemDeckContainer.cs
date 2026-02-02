using System.Collections.Generic;
using UnityEngine;

public class ItemDeckContainer : DeckContiner
{
    [Header("Config Item Deck Container")]
    [SerializeField] private int maxCardDeck;
    private int nextID;
    private List<CardInstance> cardDatas = new();
    private List<CardDeck> cards = new();

    [Header("Events")]
    public OnHideItemCardEventSO OnHideItemCardEvent;

    public override void AddCard(CardSO newCardName)
    {

        /*Debug.Log($"AddCard called on {name}");
        Debug.Log($"DeckType: {cardType}, CardType: {newCardName.cardType}");
        Debug.Log($"Current: {cardDatas.Count}, Max: {maxCardDeck}");*/
        if (cardDatas.Count >= maxCardDeck)
            return;

        CardInstance newCard = new CardInstance(newCardName, nextID++);
        cardDatas.Add(newCard);

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
        cardDatas.Remove(removeCardName);
        OnRemoveCardEvent.Raise(removeCardName);
    }
}
