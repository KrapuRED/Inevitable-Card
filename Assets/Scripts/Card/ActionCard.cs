using DG.Tweening;
using UnityEngine;

public class ActionCard : CardDeck
{
    private ActionCardUI actionCardUI;

    private void Start()
    {
        actionCardUI = GetComponent<ActionCardUI>();

        actionCardUI.SetCardUI(cardData);
    }

    #region Hovering Secetion
    public override void OnHoverEnter()
    {
        if (_isHover || _isDragging) return;

        _isHover = true;
        //Debug.Log($"Hover Enter: {cardData.cardNameMovement}");
        transform.DOMoveY(EndPosition, animationTime);
        //actionCardUI.OnHoverEnter();
        CardRenderManager.instance.OnHoverEnter(this);
        //_spriteRenderer.sortingOrder = 2;
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
        //actionCardUI.OnHoverExit();
        CardRenderManager.instance.OnHoverExit(this);
        transform.DOMoveY(StartPosition, animationTime);
        //_spriteRenderer.sortingOrder = 0;
    }
    #endregion

    public override void StartDragging(Vector2 mouseWorldPosition)
    {
        _isDragging = true;
        _isHover = false;
        _tween.Kill(transform);

        originalPosition = transform.position;

        dragOffset = transform.position - (Vector3)mouseWorldPosition;

        CardRenderManager.instance.OnDragStart(this);
        //_spriteRenderer.sortingOrder = 2;
    }

    #region DRAG SECTION
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
        CardRenderManager.instance.OnHoverExit(this);
    }
    #endregion

}
