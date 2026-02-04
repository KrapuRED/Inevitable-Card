using UnityEngine;

public class CardDeckItem : CardDeck
{
    public ItemCardUI itemcardUI;

    [Header("Events")]
    public OnHideItemCardEventSO onHideItemCard;

    private void Start()
    {
        itemcardUI = GetComponent<ItemCardUI>();
        itemcardUI.SetCardUI(cardData);
    }

    private void RemoveCard(CardInstance card)
    {
        if (card.cardData == cardData)
        {
            Destroy(gameObject);
        }
    }
}
