using System.Collections.Generic;
using UnityEngine;

public class ObtainCardControllerUI : MonoBehaviour
{
    [Header("Card Obtain")]
    [SerializeField] private List<ObtainCardUI> obtainCards;

    public void SetObtainCards(List<CardSO> cardData)
    {
        int countCard = cardData.Count;

        for (int i = 0; i < countCard; i++)
        {
            obtainCards[i].SetImageCard(cardData[i]);
        }

    }

    public void CloseCards()
    {
        foreach (var card in obtainCards)
        {
            card.ResetObtainCardUI();
        }
    }
}
