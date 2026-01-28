using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public CardSO cardData;

    [Header("State Card")]
    [SerializeField] private bool isHover;
    [SerializeField] private bool isDragging;

    [Header("Drag Config")]
    [SerializeField] private Vector2     defaultPosition;
    private Vector2     originalPosition;
    private Vector3     dragOffset;
    public  LayerMask   dropZoneLayer;

    [Header("Animation Card")]
    public float animationTime;
    [SerializeField] private float EndPosition;
    [SerializeField] private float StartPosition;

    private SpriteRenderer _spriteRenderer;
    Tween _tween;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        defaultPosition = transform.position;

        if (cardData != null && cardData.cardImage != null)
        {
            _spriteRenderer.sprite = cardData.cardImage;
        }

    }

    #region Hovering Secetion
    public void OnHoverEnter()
    {
        if (isHover || isDragging) return;

        isHover = true;
        //Debug.Log($"Hover Enter: {cardData.cardName}");
        transform.DOMoveY(EndPosition, animationTime);
        _spriteRenderer.sortingOrder = 1;
    }

    public void OnClickCard()
    {
        Debug.Log($"{cardData.cardName} is get clicked!");
    }

    public void OnHoverExit()
    {
        if (!isHover || isDragging) return;

        isHover = false;
        //Debug.Log($"Hover Exit: {cardData.cardName}");
        transform.DOMoveY(StartPosition, animationTime);
        _spriteRenderer.sortingOrder = 0;
    }
    #endregion

    #region Dragging Section
    public void StartDragging(Vector2 mouseWorldPosition)
    {
        isDragging = true;
        isHover = false;
        _tween.Kill(transform);

        originalPosition = transform.position;

        dragOffset = transform.position - (Vector3)mouseWorldPosition;

        _spriteRenderer.sortingOrder = 2;
    }

    public void Drag(Vector2 mouseWorldPosition)
    {
        if (!isDragging) return;

        transform.position = (Vector3)mouseWorldPosition + dragOffset;
    }

    //if collide with card deck collider the data get copy to card deck 

    public void EndDrag(bool droppedSuccessfully)
    {
        isDragging = false;

        DOTween.Kill(transform);

        transform.DOMove(defaultPosition, 1f).SetEase(Ease.OutQuad);

        _spriteRenderer.sortingOrder = 0;
    }
    #endregion
}
