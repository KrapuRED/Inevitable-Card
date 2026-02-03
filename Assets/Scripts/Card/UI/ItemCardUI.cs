using TMPro;
using UnityEngine;

public class ItemCardUI : CardUI
{
    public TextMeshPro cardNameText;
    public SpriteRenderer cardRenderer;

    public GameObject healStatus;
    public TextMeshPro healValue;

    public GameObject damageStatus;
    public TextMeshPro damageValue;

    public override void SetCardUI(CardSO cardData)
    {
        cardNameText.text = cardData.cardName;
        cardRenderer.sprite = cardData.cardImage;

        if (cardData.cardType == CardType.Item)
        {
            if (cardData.itemCardType == ItemCardType.HealthPotion)
            {
                healStatus.SetActive(true);
                healValue.text = cardData.itemHealAmount.ToString();
            }
            else if (cardData.itemCardType == ItemCardType.Offensive)
            {
                damageStatus.SetActive(true);
                damageValue.text = cardData.itemDamage.ToString();
            }
        }
        
    }

    public void OnHoverEnter()
    {
        cardRenderer.sortingOrder = 1;
    }

    public void OnHoverExit()
    {
        cardRenderer.sortingOrder = 0;
    }

    public void OnDrag()
    {
        cardRenderer.sortingOrder = 1;
    }
}
