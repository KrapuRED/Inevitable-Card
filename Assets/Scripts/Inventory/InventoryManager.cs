using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("Inventory Data")]
    public List<CardSO> itemCards;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (CardSO card in itemCards)
        {
            if (card.cardType == CardType.Item)
                PlayerDeckManager.instance.AddNewCard(card);
        }
    }
}
