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

    public void AddNewCard(CardSO card)
    {
        switch (card.cardType)
        {
            case CardType.Movement:
                movementDeck.AddCard(card);
                break;

            case CardType.Item:
                Debug.Log($"card type : {card.cardType}");
                itemDeck.AddCard(card);
                break;
        }
    }

    public void UsedItem(CardSO cardData)
    {
        itemDeck.RemoveCardFromDeck(cardData);
    }
}
