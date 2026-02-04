using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBattleCardDeck : BattleCardDeck
{
    public BattleDeckCardUI battleCardUI;

    [Header("State battke Card Deck")]
    [SerializeField] private int slotIndex;
    public Color InReiveCard;
    public Color OutReiveCard;
    [SerializeField] private CardInstance cardInstance;
    public CardInstance CardInstance => cardInstance;
    [SerializeField] private bool isHaveCard;
    [SerializeField] private bool isHover;
    [SerializeField] private bool isDragging;

    [Header("Aniamtion")]
    public float scaleEnd;
    public float scaleStart;
    private Vector3 orginalPosition;

    SpriteRenderer _spriteRenderer;
    BoxCollider2D _boxCollied2D;
    CanvasGroup _canvasGroup;

    private  void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollied2D = GetComponent<BoxCollider2D>();
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    public override void Init(int index)
    {
        slotIndex = index;
    }

    #region Receive Section
    public override void ReceivePlayerCard(CardInstance card, bool startHidden)
    {
        //Debug.Log($"{this.name} Succes get the data from {card.name}");
        if (card == null)
        {
            Debug.LogError("card is null");
        }
        //Debug.Log("card name : " + card.cardData.cardName);
        cardInstance = card;
        isHaveCard = true;
        ChangeStateBattleDeck(startHidden);
        battleCardUI.SetBattleDeckCard(cardInstance.cardData);
        BattleManager.instance.ChangeDataPlayerBattleDeck(card, slotIndex);
    }
    #endregion

    #region Hovering Secetion
    public override void OnHoverEnter()
    {
        if (cardInstance == null)
            return;

        if (isHover || isDragging) return;

        transform.DOScaleY(scaleEnd, 0.2f).SetEase(Ease.OutBack);
    }

    public override void OnClickCard()
    {

    }

    public override void OnHoverExit()
    {
        if (cardInstance == null)
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

    public override void ChangeStateBattleDeck(bool hidden)
    {
        if (cardInstance.cardData == null)
        {
            Debug.LogWarning($"{this.name} is not having a cardInstance");
            return;
        }

        if (cardInstance.cardData.cardType == CardType.Item)
        {
            PlayerDeckManager.instance.HideItemCard(cardInstance);
        }

        gameObject.layer = LayerMask.NameToLayer("Interact");
        _boxCollied2D.isTrigger = true;
    }

    public override void CancelCard()
    {
        isHaveCard = false;
        cardInstance = null;
        _boxCollied2D.isTrigger = false;
        transform.position = orginalPosition;
        _spriteRenderer.color = OutReiveCard;

        battleCardUI.ResetBattleCardUI();
        BattleManager.instance.ChangeDataPlayerBattleDeck(null, slotIndex);
    }

    public void ResetPlayerBattleCardDeck()
    {
        transform.DOKill(true);
        DOTween.Kill(gameObject, true);
     
        isHaveCard = false;
        cardInstance = null;
        _boxCollied2D.isTrigger = false;

        gameObject.layer = LayerMask.NameToLayer("BattleDeckDropZone");
        battleCardUI.ResetBattleCardUI();
    }

    public void ClashCardEnterAnimation(float scaleMultiplier, float time)
    {
        if (!BattleManager.instance.IsBattleOngoing)
            return;

        transform.DOKill();

        Vector3 targetScale = transform.localScale * scaleMultiplier;
        transform.DOScale(targetScale, time)
                 .SetEase(Ease.OutBack);
    }

    public void ClasCardExitAnimation(float scale, float time)
    {
        transform.DOKill();

        transform.DOScale(Vector3.one * scale, time)
            .OnComplete(() =>
            {
                if (!BattleManager.instance.IsBattleOngoing)
                    return;

                battleCardUI.UsedCard();
            });
    }

    public bool CanDragging()
    {
        return isHaveCard && cardInstance != null;
    }

}
