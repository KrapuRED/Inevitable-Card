using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OnHideButtonEventSO", menuName = "Events/UI/OnHideButtonEventSO")]
public class OnHideButtonEventSO : ScriptableObject
{
    public static Action<UIButtonContext, bool> OnShowButton;

    public void OnRaiseEvent(UIButtonContext UIContext, bool show)
    {
        OnShowButton?.Invoke(UIContext, show);
    }

    public void Register(Action<UIButtonContext, bool> listener)
    {
        OnShowButton += listener;
    }

    public void Unregister(Action<UIButtonContext, bool> listener)
    {
        OnShowButton -= listener;
    }
}
