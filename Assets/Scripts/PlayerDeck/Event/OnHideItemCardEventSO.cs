using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OnHideItemCardEventSO", menuName = "Events/On Hide Item Card Event")]
public class OnHideItemCardEventSO : ScriptableObject
{
    public Action<CardInstance> onHide;

    public void RaiseEvent(CardInstance card)
    {
        onHide?.Invoke(card);
    }

    public void Register(Action<CardInstance> listener)
    {
        onHide += listener;
    }

    public void Unregister(Action<CardInstance> listener)
    {
        onHide -= listener;
    }
}
