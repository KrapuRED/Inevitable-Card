using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OnAddCardEventSO", menuName = "Events/OnAddCardEvent")]
public class OnAddCardEventSO : ScriptableObject
{
    public Action<CardInstance> OnAddCard;

    public void Raise(CardInstance cardData)
    {
        OnAddCard?.Invoke(cardData);
    }

    public void Register(Action<CardInstance> listener)
    {
        OnAddCard += listener;
    }

    public void Unregister(Action<CardInstance> listener)
    {
        OnAddCard -= listener;
    }
}
