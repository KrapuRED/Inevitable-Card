using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleDeckCardUI : MonoBehaviour
{
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI staminaValue;
    public TextMeshProUGUI damageValue;
    public TextMeshProUGUI healValue;
    public Image borderImg;
    public Image illustationImg;

    public List<Sprite> borders;
    public GameObject damageStatus;
    public GameObject healStatus;
    public GameObject staminaStatus;

    public void SetBattleDeckCard(CardSO cardData)
    {
        //Debug.Log($"SetBattleDeckCard try to change using data {cardData.cardName}");
        ResetBattleCardUI();

        cardName.text = cardData.cardName;
        //Select Border
        if (cardData.cardType == CardType.Movement)
        {
            borderImg.sprite = borders[1];
            illustationImg.sprite = cardData.cardImage;
            staminaValue.text = cardData.StaminaCost.ToString();
            staminaStatus.SetActive(true);

            if (cardData.movementCardType == MovementCardType.Offensive)
            {
                damageStatus.SetActive(true);
                //Debug.Log($"{cardData.cardName} have damage {cardData.damageMovement}");
                damageValue.text = cardData.damageMovement.ToString();
            }
        }
        else
        {
            illustationImg.sprite= borders[2];
            if (cardData.itemCardType == ItemCardType.Offensive)
            {
                damageValue.text = cardData.damageMovement.ToString();
                damageStatus.SetActive(true);
            }
            else
            {
                healValue.text = cardData.itemHealAmount.ToString();
                healStatus.SetActive(true);
            }
        }
    }

    public void HideEnemyCard()
    {
        Debug.Log("Hide Enemy Card");

        borderImg.sprite = borders[0];
        illustationImg.sprite = borders[0];

        damageStatus.SetActive(false);

        staminaStatus.SetActive(false);

        cardName.text = "";
        staminaValue.text = "";
        damageValue.text = "";
    }

    public void ResetBattleCardUI()
    {
        //Debug.Log("reset BattleDeckCardUI");

        borderImg.sprite = borders[0];
        illustationImg.sprite = borders[0];

        damageStatus.SetActive(false);
        
        if (healStatus != null)
            healStatus.SetActive(false);

        staminaStatus.SetActive(false);

        cardName.text = "";
        staminaValue.text = "";
        damageValue.text = "";

        if (healValue != null)
            healValue.text = "";
    }
}
