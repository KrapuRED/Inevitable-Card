using DG.Tweening;
using UnityEngine;

public class CardDeckItem : CardDeck
{
    public ItemCardUI itemcardUI;
    public bool IsConsumed { get; private set; }

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

    public void CosumeCard()
    {
        IsConsumed = true;
        Debug.Log($"cosmue : {IsConsumed}");
    }
}
