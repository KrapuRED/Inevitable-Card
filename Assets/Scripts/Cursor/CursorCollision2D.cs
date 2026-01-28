using UnityEngine;
using UnityEngine.InputSystem;

public class CursorCollision2D : MonoBehaviour
{
    [Header("Config")]
    public LayerMask interactableLayer;
    public LayerMask dropZoneLayer;
    [SerializeField] private Card currentCard;
    [SerializeField] private Card lastCard;
    [SerializeField] private Card draggingCard;
    [SerializeField] private CardDeck hoveredCardDeck;
    [SerializeField] private Vector2 mousePosition;

    [Header("Cursor State")]
    [SerializeField] private bool isDragging;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector2 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        if (draggingCard != null)
        {
            draggingCard.Drag(mouseWorldPosition);

            UpdateDropZoneFeedback(mouseWorldPosition);
            return;
        }

        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition, interactableLayer);

        if (hit != null)
        {
            currentCard = hit.GetComponent<Card>();
            if (currentCard != lastCard)
            {
                lastCard?.OnHoverExit();
                currentCard?.OnHoverEnter();
                lastCard = currentCard;
            }
        }
        else
        {
            lastCard?.OnHoverExit();
            lastCard = null;
            currentCard = null;
        }
    }

    private void UpdateDropZoneFeedback(Vector2 mouseWorldPosition)
    {
        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition, dropZoneLayer);

        CardDeck newCardDeck = hit ? hit.GetComponent<CardDeck>() : null;

        if (newCardDeck != hoveredCardDeck)
        {
            hoveredCardDeck?.ExitReiveZone();
            newCardDeck?.EnterReiveZone();
            hoveredCardDeck = newCardDeck;
        }
    }

    public void OnCursorMove(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void OnClickMouse(InputAction.CallbackContext context)
    {
        Vector2 mouseWorldPosition =
           _mainCamera.ScreenToWorldPoint(mousePosition);
        
        //start dragging
        if (context.started && currentCard != null)
        {
            draggingCard = currentCard;
            draggingCard.StartDragging(mouseWorldPosition);
        }

        //End dragging
        if(context.canceled && draggingCard != null)
        {
            bool dropped = false;

            if (hoveredCardDeck != null)
            {
                hoveredCardDeck.ReceivePlayerCard(draggingCard);
                dropped = true;
            }

            draggingCard.EndDrag(dropped);
            draggingCard = null;

            hoveredCardDeck?.ExitReiveZone();
            hoveredCardDeck = null;
        }
    }
}
