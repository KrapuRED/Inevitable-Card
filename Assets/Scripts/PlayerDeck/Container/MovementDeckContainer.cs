using System.Collections.Generic;
using UnityEngine;

public class MovementDeckContainer : DeckContiner
{
    [Header("Config Movement Deck Container")]
    [SerializeField] private int maxCardDeck;
    [SerializeField] private readonly List<CardInstance> movementCards = new();
    [SerializeField] private List<CardDeck> cards = new List<CardDeck>();
    private int nextID;

    private void Start()
    {
        if (cardType != CardType.Movement)
            return;

        var cardDecks = GetComponentsInChildren<CardDeck>();

        foreach (var card in cardDecks)
        {
            if (card.cardData == null)
                continue;

            cards.Add(card);
            var instance = new CardInstance(card.cardData, nextID++);
            movementCards.Add(instance);
            card.InitializerCard(instance);
        }
    }

    public override void AddCard(CardSO newCardName)
    {
        /*
        Debug.Log($"AddCard called on {name}");
        Debug.Log($"DeckType: {cardType}, CardType: {newCardName.cardType}");
        Debug.Log($"Current: {cardDatas.Count}, Max: {maxCardDeck}");
        */

        if (movementCards.Count >= maxCardDeck)
        {
            return;
        }

        CardInstance newCard = new CardInstance(newCardName, nextID++);
        movementCards.Add(newCard);

        OnAddCardEvent.Raise(newCard);
    }

    public override void ReturnCard(CardSO cardSO)
    {
        AddCard(cardSO);
    }

    public override void RemoveCardFromDeck(CardInstance instance)
    {
        movementCards.Remove(instance);
        OnRemoveCardEvent.Raise(instance);
    }

    public override List<CardDeck> GetCards()
    {
        return cards;
    }

    public int Count => movementCards.Count;
}
