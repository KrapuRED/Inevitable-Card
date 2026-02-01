using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckUIBinder : MonoBehaviour
{
    private Dictionary<CardInstance, CardDeck> _cardViews = new();


    [Header("References")]
    public CardUIfFactory cardUIFactory;
    public DeckCardPositioner positionerItemDeck;
    public DeckCardPositioner positionerMovementDeck;

    [Header("Events")]
    public OnAddCardEventSO OnAddCardEvent;
    public OnRemoveCardEventSO OnRemoveCardEvent;
    public OnHideItemCardEventSO OnHideItemCardEvent;

    private void HandelAddedItemCard(CardInstance card)
    {
        //Debug.Log("Get Called");
        GameObject cardGO = cardUIFactory.CreateCardUI(card);
        CardDeck cardDeck = cardGO.GetComponent<CardDeck>();

        _cardViews.Add(card, cardDeck);
    
        Repositioning();
    }

    public void HandleHideItemCard(CardInstance card)
    {

    }

    private void HandleRemovedItemCard(CardInstance card)
    {
        if (!_cardViews.TryGetValue(card, out CardDeck cardDeck))
            return;

        Destroy(cardDeck);
        Repositioning();
    }

    private void Repositioning()
    {
        var cardDecks = _cardViews.Values.ToList();

        positionerItemDeck.RepositionCards(cardDecks);
    }

    private void OnEnable()
    {
        OnAddCardEvent.Register(HandelAddedItemCard);
        OnRemoveCardEvent.Register(HandleRemovedItemCard);
        OnHideItemCardEvent.Register(HandleHideItemCard);
    }

    private void OnDisable()
    {
        OnAddCardEvent.Unregister(HandelAddedItemCard);
        OnRemoveCardEvent.Unregister(HandleRemovedItemCard);
        OnHideItemCardEvent.Unregister(HandleHideItemCard);
    }
}
