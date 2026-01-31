using System;
using UnityEngine;

[System.Serializable]
public enum UIButtonContext
{
    Battle,
    GameOver,
    MainMenu,
    Dialogue
}

[CreateAssetMenu(fileName = "OnShowButtonEvent", menuName = "Events/UI/On Show Button Event")]
public class OnShowButtonEventSO : ScriptableObject
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
