using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDeckUI : MonoBehaviour
{
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI staminaValueText;
    public TextMeshProUGUI healValueText;
    public TextMeshProUGUI damageValueText;
    public Image cardIllustration;
    public GameObject statusDamage;
    public GameObject statusHeal;
    public GameObject statusStamina;

    public void SetCardDeckUI(CardSO cardData)
    {
        cardNameText.text = cardData.cardName;
        cardIllustration.sprite = cardData.cardImage;

        if (cardData.cardType == CardType.Movement)
        {
            staminaValueText.text = cardData.staminaCost.ToString();
            if (cardData.movementCardType == MovementCardType.Offensive)
            {
                //show
                damageValueText.text = cardData.damageMovement.ToString();
                statusDamage.SetActive(true);
                statusStamina.SetActive(true);
            }
            else
            {
                statusDamage.SetActive(false);
            }
        }

        if (cardData.cardType == CardType.Item)
        {
            
            if (cardData.itemCardType == ItemCardType.HealthPotion)
            {
                healValueText.text = cardData.itemHealAmount.ToString();
                statusHeal.SetActive(true);
            }
            else
            {
                damageValueText.text = cardData.itemDamage.ToString();
                statusDamage.SetActive(true);
            }
        }
    }

}
