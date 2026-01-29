using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "On Enemy Picking Card Event", menuName = "Events/Enemy/On Enemy Picking Card Event")]
public class OnEnemyPickingCardEventSO : ScriptableObject
{
    public Action<int> OnEnemyPickingCardEvent;

    public void OnRaise(int maxSlots)
    {
        OnEnemyPickingCardEvent?.Invoke(maxSlots);
    }

    public void Register(Action<int> listener)
    {
        OnEnemyPickingCardEvent += listener;
    }

    public void Unregister(Action<int> listener)
    {
        OnEnemyPickingCardEvent -= listener;

    }
}
