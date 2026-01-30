using System;
using UnityEngine;

public enum TargetType
{
    Player,
    Enemy,
    All
}

[CreateAssetMenu(fileName = "On Take Damage Event", menuName = "GlobaEvents/OnTakeDamageEvent")]
public class OnTakeDamageEventSO : ScriptableObject
{
    public Action<TargetType, int> OnRaise;

    public void RaiseEvent(TargetType target, int damageAmount)
    {
        OnRaise?.Invoke(target, damageAmount);
    }

    public void Register(Action<TargetType, int> listener)
    {
        OnRaise += listener;
    }

    public void Unregister(Action<TargetType, int> listener)
    {
        OnRaise -= listener;
    }
}
