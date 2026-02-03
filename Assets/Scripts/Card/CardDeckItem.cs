using UnityEngine;

public class CardDeckItem : CardDeck
{
    public ItemCardUI itemcardUI;

    private void Start()
    {
        itemcardUI = GetComponent<ItemCardUI>();
        itemcardUI.SetCardUI(cardData);
    }
}
