using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OnRemoveCardEventSO", menuName = "Events/OnRemoveCardEvent")]
public class OnRemoveCardEventSO : ScriptableObject
{
    public Action<CardSO> OnRemoveCard;

    public void Raise(CardSO cardData)
    {
        OnRemoveCard?.Invoke(cardData);
    }

    public void Register(Action<CardSO> listener)
    {
        OnRemoveCard += listener;
    }

    public void Unregister(Action<CardSO> listener)
    {
        OnRemoveCard -= listener;
    }
}
