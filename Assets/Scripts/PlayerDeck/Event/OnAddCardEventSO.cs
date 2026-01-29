using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OnAddCardEventSO", menuName = "Events/OnAddCardEvent")]
public class OnAddCardEventSO : ScriptableObject
{
    public Action<CardSO> OnAddCard;

    public void Raise(CardSO cardData)
    {
        OnAddCard?.Invoke(cardData);
    }

    public void Register(Action<CardSO> listener)
    {
        OnAddCard += listener;
    }

    public void Unregister(Action<CardSO> listener)
    {
        OnAddCard -= listener;
    }
}
