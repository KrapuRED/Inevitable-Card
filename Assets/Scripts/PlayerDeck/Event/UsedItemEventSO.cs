using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UsedItemEventSO", menuName = "Events/UsedItemEventSO")]
public class UsedItemEventSO : ScriptableObject
{
    public Action<CardSO> OnRaiseUsedItemEvent;

    public void Raise(CardSO cardData)
    {
        OnRaiseUsedItemEvent?.Invoke(cardData);
    }

    public void Register(Action<CardSO> listener)
    {
        OnRaiseUsedItemEvent += listener;
    }

    public void UnRegister(Action<CardSO> listener)
    {
        OnRaiseUsedItemEvent -= listener;
    }
}
