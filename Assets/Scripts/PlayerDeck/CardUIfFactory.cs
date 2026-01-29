using UnityEngine;

public class CardUIfFactory : MonoBehaviour
{
    [Header("Config CardUI Factory")]
    public GameObject PrefabCard;
    public Transform movenentDeckUI;
    public Transform itemDeckUI;

    public GameObject CreateCardUI(CardSO cardData)
    {
        Transform container = cardData.cardType == 
            CardType.Movement ? movenentDeckUI : itemDeckUI;

        GameObject cardGO = Instantiate(PrefabCard, container);

        CardDeck card = cardGO.GetComponent<CardDeck>();
        card.InitializerCard(cardData);

        return cardGO;
    }
}
