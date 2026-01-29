using UnityEngine;

public class CardUIfFactory : MonoBehaviour
{
    [Header("Config CardUI Factory")]
    public GameObject PrefabCard;
    public Transform movenentDeckUI;
    public Transform itemDeckUI;

    public GameObject CreateCardUI(CardInstance card)
    {
        Transform container = card.cardData.cardType == 
            CardType.Movement ? movenentDeckUI : itemDeckUI;

        GameObject cardGO = Instantiate(PrefabCard, container);

        CardDeck cardDeck = cardGO.GetComponent<CardDeck>();
        cardDeck.InitializerCard(card);

        return cardGO;
    }
}
