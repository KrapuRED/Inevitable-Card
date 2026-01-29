using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckUIBinder : MonoBehaviour
{
    private Dictionary<CardSO, Card> itemCardViews = new();

    [Header("References")]
    public CardUIfFactory cardUIFactory;
    public DeckCardPositioner positionerItemDeck;
    public DeckCardPositioner positionerMovementDeck;

    [Header("Events")]
    public OnAddCardEventSO OnAddCardEvent;
    public OnRemoveCardEventSO OnRemoveCardEvent;

    private void HandelAddedCard(CardSO cardData)
    {
        //Debug.Log("Get Called");
        GameObject cardGO = cardUIFactory.CreateCardUI(cardData);
        Card card = cardGO.GetComponent<Card>();

        itemCardViews.Add(cardData, card);
    
        Repositioning();
    }

    private void HandleRemovedCard(CardSO cardData)
    {
        if (!itemCardViews.TryGetValue(cardData, out Card card))
            return;

        Destroy(card.gameObject);
        itemCardViews.Remove(cardData);

        Repositioning();
    }

    private void Repositioning()
    {
        positionerItemDeck.RepositionCards(itemCardViews.Values.ToList<Card>());
    }

    private void OnEnable()
    {
        OnAddCardEvent.Register(HandelAddedCard);
        OnRemoveCardEvent.Register(HandleRemovedCard);
    }

    private void OnDisable()
    {
        OnAddCardEvent.Unregister(HandelAddedCard);
        OnRemoveCardEvent.Unregister(HandleRemovedCard);
    }
}
