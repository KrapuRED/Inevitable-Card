using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OnRemoveCardEventSO", menuName = "Events/OnRemoveCardEvent")]
public class OnRemoveCardEventSO : ScriptableObject
{
    public Action<CardInstance> OnRemoveCard;

    public void Raise(CardInstance cardData)
    {
        OnRemoveCard?.Invoke(cardData);
    }

    public void Register(Action<CardInstance> listener)
    {
        OnRemoveCard += listener;
    }

    public void Unregister(Action<CardInstance> listener)
    {
        OnRemoveCard -= listener;
    }
}
