using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckUIBinder : MonoBehaviour
{
    private Dictionary<CardInstance, CardDeck> _cardItemViews = new();
    private Dictionary<CardInstance, CardDeck> _cardActionViews = new();

    [Header("References")]
    public CardUIfFactory cardUIFactory;
    public DeckCardPositioner positionerItemDeck;
    public MovementDeckContainer movementContainer;
    public DeckCardPositioner positionerActionDeck;

    [Header("Events")]
    public OnAddCardEventSO OnAddCardEvent;
    public OnRemoveCardEventSO OnRemoveCardEvent;
    public OnHideItemCardEventSO OnHideItemCardEvent;
    public RegisterCardEventSO OnRegisterCardEvent;
    public OnRepositioningCardEventSO OnRepositioningCardEvent;

  
    private void HandelAddedItemCard(CardInstance card)
    {
        //Debug.Log("Get Called");
        GameObject cardGO = cardUIFactory.CreateCardUI(card);
        CardDeck cardDeck = cardGO.GetComponent<CardDeck>();

        _cardItemViews.Add(card, cardDeck);
        RepositionItemDeck();
    }

    public void HandleHideItemCard(CardInstance card)
    {

    }

    private void HandleRemovedItemCard(CardInstance card)
    {
        if (!_cardItemViews.TryGetValue(card, out CardDeck cardDeck))
            return;

        // 1 REMOVE from dictionary FIRST
        _cardItemViews.Remove(card);

        // 2️ DESTROY the GameObject
        Destroy(cardDeck.gameObject);

        // 3️ NOW reposition safely
        RepositionItemDeck();
    }

    public void RegisterCardView(CardDeck view)
    {
        if (view.Instance == null)
            return;

        if (view.cardData.cardType == CardType.Movement)
        {
            //Debug.Log($"Succes register : {view.cardData.cardNameMovement}");
            _cardActionViews[view.Instance] = view;
        }

        RepositionActionDeck();
    }

    private void HandleRepositioningActionCard(CardInstance cardData, CardDeck cardGO)
    {
        if (cardData.cardData.cardType == CardType.Movement)
        {
            _cardActionViews.Add(cardData, cardGO);
        }
            RepositionActionDeck();
    }

    private  void RepositionActionDeck()
    {
        positionerActionDeck.RepositionCards(
            _cardActionViews.Values.ToList()
        );
    }

    private void RepositionItemDeck()
    {
        positionerItemDeck.RepositionCards(
            _cardItemViews.Values.ToList()
        );

        /*for (int i = 0; i < _cardActionViews.Values.ToList().Count; i++)
        {
            CardRenderManager.instance.SetBaseOrder(cards[i], i);
        }*/
    }

    private void OnEnable()
    {
        OnAddCardEvent.Register(HandelAddedItemCard);
        OnRemoveCardEvent.Register(HandleRemovedItemCard);
        OnHideItemCardEvent.Register(HandleHideItemCard);
        OnRepositioningCardEvent.Register(HandleRepositioningActionCard);
        OnRegisterCardEvent.Register(RegisterCardView);
    }

    private void OnDisable()
    {
        OnAddCardEvent.Unregister(HandelAddedItemCard);
        OnRemoveCardEvent.Unregister(HandleRemovedItemCard);
        OnHideItemCardEvent.Unregister(HandleHideItemCard);
    }
}
