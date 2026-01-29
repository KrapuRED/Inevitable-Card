using System.Collections.Generic;
using UnityEngine;

public class MovementDeckContainer : DeckContiner
{
    [Header("Config Movement Deck Container")]
    [SerializeField] private int maxCardDeck;
    [SerializeField] private List<CardInstance> movementCards = new();
    private int nextID;

    private void Start()
    {
        if (cardType != CardType.Movement)
            return;

        CardDeck[] movements = GetComponentsInChildren<CardDeck>(true);

        foreach (CardDeck card in movements)
        {
            if (card.cardData == null)
            {
                Debug.LogWarning($"{card.name} has no CardSO assigned");
                continue;
            }

            // 1. Create runtime instance
            CardInstance instance = new CardInstance(card.cardData, nextID++);

            // 2. Store it in the container
            movementCards.Add(instance);

            // 3. Bind instance -> card UI
            card.InitializerCard(instance);
        }
    }

    public override void AddCard(CardSO newCardName)
    {
        /*
        Debug.Log($"AddCard called on {name}");
        Debug.Log($"DeckType: {cardType}, CardType: {newCardName.cardType}");
        Debug.Log($"Current: {cards.Count}, Max: {maxCardDeck}");
        */

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

    public int Count => movementCards.Count;
}
