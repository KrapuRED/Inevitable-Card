using DG.Tweening;
using UnityEngine;

public class CardDeck : Card
{
    public CardSO cardData;

    [Header("State Card")]
    [SerializeField] private bool isHover;
    [SerializeField] private bool isDragging;
    [SerializeField] private CardDeckUI cardUI;

    [Header("Drag Config")]
    [SerializeField] private Vector2 defaultPosition;
    private Vector2 originalPosition;
    private Vector3 dragOffset;
    public LayerMask dropZoneLayer;
    public RegisterCardEventSO registerCardEvent;

    [Header("Animation Card")]
    public float animationTime;
    [SerializeField] private float EndPosition;
    [SerializeField] private float StartPosition;

    public CardInstance Instance { get; private set; }

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxcollied2D;
    Tween _tween;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxcollied2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        defaultPosition = transform.position;

        if (cardUI != null)
            cardUI.SetCardDeckUI(cardData);
    }

    public override void UpdateBoxCollider2D(float visibleWidth)
    {
        if (_boxcollied2D == null)
        {
            Debug.LogError($"{this.name} is missing a BoxCollider2D");
            return;
        }

        Vector2 size = _boxcollied2D.size;
        size.x = visibleWidth;
        _boxcollied2D.size = size;

        Vector2 offset = _boxcollied2D.offset;
        offset.x = visibleWidth * 0.5f;
        _boxcollied2D.offset = offset;
    }

    public override void InitializerCard(CardInstance newCardSO)
    {
        cardData = newCardSO.cardData;
        Instance = newCardSO;
        registerCardEvent.OnRegisterCard(this);

        //Debug.Log($"Card Name : {Instance.cardData.cardName} Card ID : {Instance.ID}");

        if (cardData.cardType == CardType.Item && cardData.itemCardType == ItemCardType.Offensive)
        {
            _spriteRenderer.color = Color.red;
        }
        else if (cardData.cardType == CardType.Item && cardData.itemCardType == ItemCardType.HealthPotion)
        {
            _spriteRenderer.color = Color.green;
        }
    }


    #region Hovering Secetion
    public override void OnHoverEnter()
    {
        if (isHover || isDragging) return;

        isHover = true;
        //Debug.Log($"Hover Enter: {cardData.cardName}");
        transform.DOMoveY(EndPosition, animationTime);
        _spriteRenderer.sortingOrder = 1;
    }

    public override void OnClickCard()
    {
        Debug.Log($"{cardData.cardName} is get clicked!");
    }

    public override void OnHoverExit()
    {
        if (!isHover || isDragging) return;

        isHover = false;
        //Debug.Log($"Hover Exit: {cardData.cardName}");
        transform.DOMoveY(StartPosition, animationTime);
        _spriteRenderer.sortingOrder = 0;
    }
    #endregion

    #region Dragging Section
    public override void StartDragging(Vector2 mouseWorldPosition)
    {
        isDragging = true;
        isHover = false;
        _tween.Kill(transform);

        originalPosition = transform.position;

        dragOffset = transform.position - (Vector3)mouseWorldPosition;

        _spriteRenderer.sortingOrder = 2;
    }

    public override void Drag(Vector2 mouseWorldPosition)
    {
        if (!isDragging) return;

        transform.position = (Vector3)mouseWorldPosition + dragOffset;
    }

    //if collide with card deck collider the data get copy to card deck 

    public override void EndDrag(bool droppedSuccessfully)
    {
        isDragging = false;
        DOTween.Kill(transform);

        transform.DOMove(defaultPosition, 1f).SetEase(Ease.OutQuad);
        _spriteRenderer.sortingOrder = 0;
    }
    #endregion
}
