using UnityEngine;
using UnityEngine.InputSystem;

public class CursorCollision2D : MonoBehaviour
{
    [Header("Config")]
    public LayerMask interactableLayer;
    public LayerMask BattleDeckDropZoneLayer;
    public LayerMask ContainerDropZoneLayer;

    [Header("Card Deck")]
    [SerializeField] private CardDeck currentCard;
    [SerializeField] private CardDeck lastCard;
    [SerializeField] private CardDeck draggingCard;

    [Header("Battle Deck Card")]
    [SerializeField] private BattleCardDeck currentBCD;
    [SerializeField] private BattleCardDeck lastBCD;
    [SerializeField] private BattleCardDeck draggingBCD;
    [SerializeField] private Vector2 mousePosition;

    [Header("DropZone")]
    [SerializeField] private BattleCardDeck hoveredCardDeck;
    [SerializeField] private DeckContiner containerDeckCard;

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

        if (draggingBCD != null)
        {
            draggingBCD.Drag(mouseWorldPosition);
            UpdateDropZoneFeedback(mouseWorldPosition);
            return;
        }

        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition, interactableLayer);

        if (hit != null)
        {
            currentCard = hit.GetComponent<CardDeck>();
            currentBCD = hit.GetComponent<BattleCardDeck>();

            if (currentCard != lastCard)
            {
                lastCard?.OnHoverExit();
                currentCard?.OnHoverEnter();
                lastCard = currentCard;
            }

            if (currentBCD != lastBCD)
            {
                lastBCD?.OnHoverExit();
                currentBCD?.OnHoverEnter();
                lastBCD = currentBCD;
            }
        }
        else
        {
            lastCard?.OnHoverExit();
            lastBCD?.OnHoverExit();
            lastCard = null;
            currentCard = null;
            lastBCD = null;
            currentBCD = null;
        }
    }

    private void UpdateDropZoneFeedback(Vector2 mouseWorldPosition)
    {
        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition, BattleDeckDropZoneLayer);

        #region Battle Deck Card
        Collider2D battleHit =
        Physics2D.OverlapPoint(mouseWorldPosition, BattleDeckDropZoneLayer);

        BattleCardDeck newBattleDeck =
            battleHit ? battleHit.GetComponent<BattleCardDeck>() : null;

        if (newBattleDeck != hoveredCardDeck)
        {
            hoveredCardDeck?.ExitReceiveZone();
            newBattleDeck?.EnterReceiveZone();
            hoveredCardDeck = newBattleDeck;
        }

        #endregion
        #region Deck Container
        Collider2D containerHit =
        Physics2D.OverlapPoint(mouseWorldPosition, ContainerDropZoneLayer);

        DeckContiner newContainer =
            containerHit ? containerHit.GetComponentInParent<DeckContiner>() : null;

        if (newContainer != containerDeckCard)
        {
            containerDeckCard = newContainer;
        }
        #endregion
    }

    public void OnCursorMove(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void OnClickMouse(InputAction.CallbackContext context)
    {
        Vector2 mouseWorldPosition =
           _mainCamera.ScreenToWorldPoint(mousePosition);

        #region Card Deck Dragging
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

            hoveredCardDeck?.ExitReceiveZone();
            hoveredCardDeck = null;
        }
        #endregion

        #region Battle Card Dragging
        //start dragging
        if (context.started && currentBCD != null && currentBCD.CanDragging())
        {
            draggingBCD = currentBCD;
            draggingBCD.StartDragging(mouseWorldPosition);
        }

        //End dragging
        if (context.canceled && draggingBCD != null)
        {
            bool dropped = false;

            if (containerDeckCard != null &&
                containerDeckCard.cardType == draggingBCD.cardData.cardType)
            {
                containerDeckCard.AddCard(draggingBCD.cardData);
                draggingBCD.CancelCard();
                dropped = true;
            }

            draggingBCD.EndDrag(dropped);
            draggingBCD = null;

            hoveredCardDeck?.ExitReceiveZone();
            hoveredCardDeck = null;
        }
        #endregion
    }
}
