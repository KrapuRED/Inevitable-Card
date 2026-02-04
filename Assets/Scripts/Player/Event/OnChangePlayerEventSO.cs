using System;
using UnityEngine;

[CreateAssetMenu(fileName = "On Change Player Event", menuName = "Events/Player/On Change Player Event")]
public class OnChangePlayerEventSO : ScriptableObject
{
    public Action<Player> OnChangeEnemy;

    public void RaiseEvent(Player newPlayer)
    {
        OnChangeEnemy?.Invoke(newPlayer);
    }

    public void Register(Action<Player> listener)
    {
        OnChangeEnemy += listener;
    }

    public void Unregister(Action<Player> listener)
    {
        OnChangeEnemy -= listener;
    }
}
