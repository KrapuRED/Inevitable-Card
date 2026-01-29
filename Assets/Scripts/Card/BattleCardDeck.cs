using UnityEngine;

public class BattleCardDeck : MonoBehaviour
{
    [Header("State battke Card Deck")]
    [SerializeField] private bool isAbleReiveCard;
    public Color InReiveCard;
    public Color OutReiveCard;
    [SerializeField] private CardSO _cardData;

    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void EnterReiveZone()
    {
        if (isAbleReiveCard) return;

        isAbleReiveCard = true;
        _spriteRenderer.color = InReiveCard;
    }

    public void ReceivePlayerCard(Card card)
    {
        Debug.Log($"{this.name} Succes get the data from {card.name}");
        _cardData = card.cardData;
        Debug.Log("card name : " + _cardData.cardName);
    }

    public void ExitReiveZone()
    {
        if (!isAbleReiveCard) return;

        isAbleReiveCard = false;
        _spriteRenderer.color = OutReiveCard;
    }
}
