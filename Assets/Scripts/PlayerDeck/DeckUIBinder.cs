using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckUIBinder : MonoBehaviour
{
    private Dictionary<CardSO, CardDeck> itemCardViews = new();

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
        if (itemCardViews.TryGetValue(cardData, out CardDeck card))
        {
            card.gameObject.SetActive(true);
            Repositioning();
            return;
        }

        GameObject cardGO = cardUIFactory.CreateCardUI(cardData);
        CardDeck cardDeck = cardGO.GetComponent<CardDeck>();

        itemCardViews.Add(cardData, cardDeck);
    
        Repositioning();
    }

    private void HandleRemovedCard(CardSO cardData)
    {
        if (!itemCardViews.TryGetValue(cardData, out CardDeck card))
            return;

        card.gameObject.SetActive(false);
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
