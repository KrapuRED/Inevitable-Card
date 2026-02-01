using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "On RepositionItemDeck Card Event", menuName = "Global Events/On RepositionItemDeck Card Event")]
public class OnRepositioningCardEventSO : ScriptableObject
{
    public UnityAction<CardInstance, CardDeck> OnRepositioning;

    public void OnRaise(CardInstance cardData, CardDeck cardGO)
    {
        OnRepositioning?.Invoke(cardData, cardGO);
    }

    public void Register(UnityAction<CardInstance, CardDeck> listener)
    {
        OnRepositioning += listener;
    }

    public void Unregister(UnityAction<CardInstance, CardDeck> listener)
    {
        OnRepositioning -= listener;
    }
}
