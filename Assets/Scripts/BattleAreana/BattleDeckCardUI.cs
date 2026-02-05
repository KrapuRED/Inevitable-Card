using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDeckCardUI : MonoBehaviour
{
    public TextMeshPro cardNameMovement;
    public TextMeshPro cardNameitem;
    public SpriteRenderer borderImg;
    public SpriteRenderer illustationImg;
    public List<Sprite> borders;

    [Header("Visual Effects")]
    public GameObject useCardEffect;
    public GameObject hideCardEffect;

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
        //Debug.Log($"SetBattleDeckCard try to change using data {cardData.cardNameMovement}");
        ResetBattleCardUI();
        illustationImg.sprite = cardData.cardImage;

        borderImg.sprite = cardData.cardType == CardType.Movement
            ? borders[1]
            : borders[2];

        if (hideCardEffect != null)
        {
            hideCardEffect.SetActive(false);
        }

        ShowStatus(cardData);
    }

    private void ShowStatus(CardSO cardData)
    {
        if (cardData.cardType == CardType.Movement)
        {

            cardNameMovement.text = cardData.cardName;
            if (cardData.damageMovement > 0 && cardData.staminaCost > 0)
            {
                staminaDoubleValue.text = cardData.staminaCost.ToString();
                damageDoubleValue.text = cardData.damageMovement.ToString();
                doubleStatus.SetActive(true);
            }
            else if (cardData.staminaCost > 0)
            {
                staminaSingelValue.text = cardData.staminaCost.ToString();
                staminaStatus.SetActive(true);
            }
        }
        else if (cardData.cardType == CardType.Item)
        {

            cardNameitem.text = cardData.cardName;
            if (cardData.itemHealAmount > 0)
            {
                healSingelValue.text = cardData.itemHealAmount.ToString();
                healStatus.SetActive(true);
            }
            else if (cardData.itemDamage > 0)
            {
                damageSingelValue.text= cardData.itemDamage.ToString();
                damageStatus.SetActive(true);
            }
        }
    }

    public void HideEnemyCard()
    {
        //Debug.Log("Hide Enemy Card");

        hideCardEffect.SetActive(true);

        doubleStatus.SetActive(false);

        staminaStatus.SetActive(false);

        healStatus.SetActive(false);

        cardNameMovement.text = "";
        staminaDoubleValue.text = "";
        healSingelValue.text = "";
        damageDoubleValue.text = "";
    }

    public void UsedCard()
    {
        useCardEffect.SetActive(true);
    }

    public void ResetBattleCardUI()
    {
        //Debug.Log($"[RESET UI] {gameObject.name}");
        useCardEffect.SetActive(false);
        borderImg.sprite = borders[0];
        illustationImg.sprite = borders[0];

        doubleStatus.SetActive(false);
        
        if (damageStatus != null)
            damageStatus.SetActive(false);

        staminaStatus.SetActive(false);

        doubleStatus.SetActive(false);

        staminaStatus.SetActive(false);

        healStatus.SetActive(false);

        cardNameMovement.text = "";
        
        if (cardNameitem != null)
            cardNameitem.text = "";

        staminaDoubleValue.text = "";
        healSingelValue.text = "";
        damageDoubleValue.text = "";

    }
}
