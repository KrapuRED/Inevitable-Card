using UnityEngine;

public class PlayerDeckManager : MonoBehaviour
{
    public static PlayerDeckManager instance;

    [Header("Player Deck")]
    public DeckContiner movementDeck;
    [SerializeField] private int TotalMovementCardDeck;
    public DeckContiner itemDeck;
    [SerializeField] private int TotalItemCardDeck;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddNewCard(CardSO cardData)
    {
        switch (cardData.cardType)
        {
            case CardType.Movement:
                movementDeck.AddCard(cardData);
                break;

            case CardType.Item:
                //Debug.Log($"cardData type : {cardData.cardType}");
                itemDeck.AddCard(cardData);
                break;
        }
    }

    public void RemoveCard(CardSO cardData)
    {
        switch (cardData.cardType)
        {
            case CardType.Movement:
                movementDeck.RemoveCardFromDeck(cardData);
                break;

            case CardType.Item:
                //Debug.Log($"cardData type : {cardData.cardType}");
                itemDeck.RemoveCardFromDeck(cardData);
                break;
        }
    }

    public void UsedItem(CardSO cardData)
    {
        itemDeck.RemoveCardFromDeck(cardData);
    }
}
