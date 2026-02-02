using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDeckCardUI : MonoBehaviour
{
    public TextMeshPro cardName;
    public SpriteRenderer borderImg;
    public SpriteRenderer illustationImg;
    public List<Sprite> borders;

    [Header("Double Status")]
    public GameObject doubleStatus;
    public TextMeshPro staminaDoubleValue;
    public TextMeshPro damageDoubleValue;

    [Header("Singel Damage")]
    public GameObject damageStatus;
    public TextMeshPro damageSingelValue;

    [Header("Singel Stamina")]
    public GameObject staminaStatus;
    public TextMeshPro staminaSingelValue;

    [Header("Singel Stamina")]
    public GameObject healStatus;
    public TextMeshPro healSingelValue;

    public void SetBattleDeckCard(CardSO cardData)
    {
        //Debug.Log($"SetBattleDeckCard try to change using data {cardData.cardName}");
        ResetBattleCardUI();

        cardName.text = cardData.cardName;
        illustationImg.sprite = cardData.cardImage;

        borderImg.sprite = cardData.cardType == CardType.Movement
            ? borders[1]
            : borders[2];

        ShowStatus(cardData);
    }

    private void ShowStatus(CardSO cardData)
    {
        if (cardData.damageMovement == 0 && cardData.StaminaCost == 0)
        {
            doubleStatus.SetActive(true);

        }
    }

    public void HideEnemyCard()
    {
        Debug.Log("Hide Enemy Card");

        borderImg.sprite = borders[0];
        illustationImg.sprite = borders[0];

        doubleStatus.SetActive(false);

        staminaStatus.SetActive(false);

        cardName.text = "";
        staminaDoubleValue.text = "";
        damageDoubleValue.text = "";
    }

    public void ResetBattleCardUI()
    {
        //Debug.Log("reset BattleDeckCardUI");

        borderImg.sprite = borders[0];
        illustationImg.sprite = borders[0];

        doubleStatus.SetActive(false);
        
        if (damageStatus != null)
            damageStatus.SetActive(false);

        staminaStatus.SetActive(false);

        cardName.text = "";
        staminaSingelValue.text = "";
        damageDoubleValue.text = "";

        if (damageSingelValue != null)
            damageSingelValue.text = "";
    }
}
