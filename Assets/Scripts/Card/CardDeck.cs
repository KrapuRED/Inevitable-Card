using DG.Tweening;
using UnityEngine;

public class CardDeck : Card
{
    public CardSO cardData;

    [Header("State Card")]
    [SerializeField] private bool isHover;
    [SerializeField] private bool isDragging;

    [Header("Drag Config")]
    [SerializeField] private Vector2 defaultPosition;
    private Vector2 originalPosition;
    private Vector3 dragOffset;
    public LayerMask dropZoneLayer;

    [Header("Animation Card")]
    public float animationTime;
    [SerializeField] private float EndPosition;
    [SerializeField] private float StartPosition;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxcollied2D;
    Tween _tween;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxcollied2D = GetComponent<BoxCollider2D>();
        defaultPosition = transform.position;

        if (cardData != null && cardData.cardImage != null)
        {
            _spriteRenderer.sprite = cardData.cardImage;
        }
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

    public override void InitializerCard(CardSO newCardSO)
    {
        cardData = newCardSO;
    }

    public override void UseItem()
    {

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
