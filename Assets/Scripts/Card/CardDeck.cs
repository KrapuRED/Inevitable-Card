using DG.Tweening;
using UnityEngine;

public class CardDeck : Card
{
    public CardSO cardData;

    [Header("State Card")]
    public int BaseLayerOrder { get; private set; }
    [SerializeField] protected bool _isHover;
    [SerializeField] protected bool _isDragging;
    [SerializeField] protected CardDeckUI cardUI;

    [Header("Drag Config")]
    protected Vector2 originalPosition;
    protected Vector3 dragOffset;
    public LayerMask dropZoneLayer;
    public RegisterCardEventSO registerCardEvent;

    [Header("Animation Card")]
    public float animationTime;
    [SerializeField] protected float EndPosition;
    [SerializeField] protected float StartPosition;

    public CardInstance Instance { get; private set; }

    protected SpriteRenderer _spriteRenderer;
    protected BoxCollider2D _boxcollied2D;
    protected Tween _tween;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxcollied2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if (cardUI != null)
            cardUI.SetCardDeckUI(cardData);
    }

    public void SetBaseLayerOrder(int order)
    {
        BaseLayerOrder = order;
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
        if (registerCardEvent != null)
            registerCardEvent.OnRegisterCard(this);
        else
            Debug.LogError($"{name} registerCardEvent is NULL!");
    }


    #region Hovering Secetion
    public override void OnHoverEnter()
    {
        if (_isHover || _isDragging) return;

        _isHover = true;
        //Debug.Log($"Hover Enter: {cardData.cardNameMovement}");
        transform.DOMoveY(EndPosition, animationTime);
        //_spriteRenderer.sortingOrder = 1;
        SoundEffectManager.instance.PlaySoundEffectOneClip("CardDeckHover");

    }

    public override void OnClickCard()
    {
        Debug.Log($"{cardData.cardName} is get clicked!");
    }

    public override void OnHoverExit()
    {
        if (!_isHover || _isDragging) return;

        _isHover = false;
        //Debug.Log($"Hover Exit: {cardData.cardNameMovement}");
        transform.DOMoveY(StartPosition, animationTime);
        //_spriteRenderer.sortingOrder = 0;
    }
    #endregion

    #region Dragging Section
    public override void StartDragging(Vector2 mouseWorldPosition)
    {
        _isDragging = true;
        _isHover = false;
        _tween.Kill(transform);

        originalPosition = transform.position;

        dragOffset = transform.position - (Vector3)mouseWorldPosition;

        //_spriteRenderer.sortingOrder = 2;
    }

    public override void Drag(Vector2 mouseWorldPosition)
    {
        if (!_isDragging) return;

        transform.position = (Vector3)mouseWorldPosition + dragOffset;
    }

    //if collide with card deck collider the data get copy to card deck 

    public override void EndDrag(bool droppedSuccessfully)
    {
        _isDragging = false;
        DOTween.Kill(transform);

        transform.DOMove(originalPosition, 1f).SetEase(Ease.OutQuad);
        //_spriteRenderer.sortingOrder = 0;
    }
    #endregion
}
