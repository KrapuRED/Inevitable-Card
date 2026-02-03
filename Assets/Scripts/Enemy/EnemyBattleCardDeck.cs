using DG.Tweening;
using UnityEngine;

public class EnemyBattleCardDeck :BattleCardDeck
{
    public BattleDeckCardUI battleCardUI;

    [Header("State battke Card Deck")]
    [SerializeField] private int slotIndex;
    [SerializeField] private bool isAbleReiveCard;
    public Color InReiveCard;
    public Color OutReiveCard;
    [SerializeField] private CardInstance cardInstance;
    public CardInstance CardInstance => cardInstance;
    [SerializeField] private bool isHaveCard;
    [SerializeField] private bool isHover;
    [SerializeField] private bool isDragging;
    [SerializeField] private bool isHiddenCard;

    [Header("Aniamtion")]
    public float scaleEnd;
    public float scaleStart;
    private Vector3 orginalPosition;

    SpriteRenderer _spriteRenderer;
    BoxCollider2D _boxCollied2D;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollied2D = GetComponent<BoxCollider2D>();
    }

    public override void Init(int index)
    {
        slotIndex = index;
    }

    #region Receive Section
    public override void EnterReceiveZone()
    {
        if (isAbleReiveCard) return;

        isAbleReiveCard = true;
        _spriteRenderer.color = InReiveCard;
    }

    public override void ReceivePlayerCard(CardInstance card)
    {
        //Debug.Log($"{this.name} Succes get the data from {card.name}");
        if (card == null)
        {
            Debug.LogError("card is null");
        }
        //Debug.Log("card name : " + card.cardData.cardName);
        cardInstance = card;
        isHaveCard = true;
        
        if (battleCardUI != null)
            battleCardUI.SetBattleDeckCard(cardInstance.cardData);

        BattleManager.instance.ChangeDataEnemyBattleDeck(card, slotIndex);
        ChangeStateBattleDeck();
    }

    public override void ExitReceiveZone()
    {
        if (!isAbleReiveCard) return;

        isAbleReiveCard = false;
        _spriteRenderer.color = OutReiveCard;
    }

    #endregion

    #region Hovering Secetion
    public override void OnHoverEnter()
    {
        if (cardInstance.cardData == null)
            return;

        if (isHover || isDragging) return;

        transform.DOScaleY(scaleEnd, 0.2f).SetEase(Ease.OutBack);
    }

    public override void OnClickCard()
    {

    }

    public override void OnHoverExit()
    {
        if (cardInstance.cardData == null)
            return;

        if (!isHover || isDragging) return;

        transform.DOScale(scaleStart, 0.2f).SetEase(Ease.OutBack);
    }
    #endregion

    #region Dragging Section
    public override void StartDragging(Vector2 mouseWorldPosition)
    {
        if (!isHaveCard) return;

        isDragging = true;
        orginalPosition = transform.position;

        gameObject.layer = LayerMask.NameToLayer("Interact");
    }

    public override void Drag(Vector2 mouseWorldPosition)
    {
        if (!isDragging) return;
        transform.position = mouseWorldPosition;
    }

    public override void EndDrag(bool droppedSuccessfully)
    {
        isDragging = false;

        if (!droppedSuccessfully)
        {
            transform.position = orginalPosition;
            return;
        }

        gameObject.layer = LayerMask.NameToLayer("BattleDeckDropZone");
    }
    #endregion

    public override void ChangeStateBattleDeck()
    {
        if (cardInstance.cardData != null)
        {
            gameObject.layer = LayerMask.NameToLayer("Interact");
            _spriteRenderer.color = InReiveCard;

            if (isHiddenCard)
                battleCardUI.HideEnemyCard();
        }
    }

    public void ShowCard()
    {
        isHiddenCard = false;

        if (battleCardUI != null && cardInstance != null)
            battleCardUI.SetBattleDeckCard(cardInstance.cardData);
    }

    public void ClashCardEnterAnimation(float scaleMultiplier, float time)
    {
        transform.DOKill();

        ShowCard();

        Vector3 targetScale = transform.localScale * scaleMultiplier;
        transform.DOScale(targetScale, time)
                 .SetEase(Ease.OutBack);
    }

    public void ClasCardExitAnimation(float scale, float time)
    {
        transform.DOScale(Vector3.one * scale, time);
        battleCardUI.UsedCard();
    }

    public override void CancelCard()
    {
        isHaveCard = false;
        cardInstance = null;
        isHiddenCard = false;
        isAbleReiveCard = false;
        _spriteRenderer.color = OutReiveCard;
    }

    public void SetHiddenCard()
    {
        isHiddenCard = true;
    }
    public bool IsHiddenCard() { return isHiddenCard; }
}
