using TMPro;
using UnityEngine;

public class ActionCardUI : CardUI
{
    public TextMeshPro cardNameText;
    public SpriteRenderer cardRenderer;
    public TextMeshPro staminaValue;
    public TextMeshPro damageValue;

    public override void SetCardUI(CardSO cardData)
    {
        cardNameText.text = cardData.cardName;
        cardRenderer.sprite = cardData.cardImage;
        staminaValue.text = cardData.StaminaCost.ToString();

        if (damageValue != null)
            damageValue.text = cardData.damageMovement.ToString();
    }

    public void OnHoverEnter()
    {
        cardRenderer.sortingOrder = 1;
    }

    public void OnHoverExit()
    {
        cardRenderer.sortingOrder = -1;
    }

    public void OnDrag()
    {
        cardRenderer.sortingOrder = 1;
    }

}
