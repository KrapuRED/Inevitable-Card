using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OnTakingHealEventSO", menuName = "Global Events/OnTakingHealEventSO")]
public class OnTakingHealEventSO : ScriptableObject
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

