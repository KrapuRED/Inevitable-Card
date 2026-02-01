using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDeckUI : MonoBehaviour
{
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI staminaValueText;
    public TextMeshProUGUI damageValueText;
    public Image cardIllustration;
    public GameObject statusStamina;

    public void SetCardDeckUI(CardSO cardData)
    {
        cardNameText.text = cardData.cardName;
        staminaValueText.text = cardData.StaminaCost.ToString();
        cardIllustration.sprite = cardData.cardImage;

        if (cardData.movementCardType == MovementCardType.Offensive)
        {
            //show
            damageValueText.text = cardData.damageMovement.ToString();
            statusStamina.SetActive(true);
        }
        else
        {
            statusStamina.SetActive(false);
        }

    }

}
