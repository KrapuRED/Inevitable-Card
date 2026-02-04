using UnityEngine;

public class PlayerDeckManager : MonoBehaviour
{
    public static PlayerDeckManager instance;

    [Header("Player Deck")]
    public MovementDeckContainer movementDeck;
    [SerializeField] private int TotalMovementCardDeck;
    public ItemDeckContainer itemDeck;
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
                //Debug.Log($"card type : {card.cardType}");
                itemDeck.AddCard(cardData);
                break;
        }
    }

    public void HideItemCard(CardInstance card)
    {
        itemDeck.HideItemCard(card);
    }

    public void UsedItem(CardInstance cardData)
    {
        itemDeck.RemoveCardFromDeck(cardData);
    }
}
