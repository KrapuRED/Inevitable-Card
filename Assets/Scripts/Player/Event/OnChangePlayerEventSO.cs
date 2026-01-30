using System;
using UnityEngine;

[CreateAssetMenu(fileName = "On Change Player Event", menuName = "Events/Player/On Change Player Event")]
public class OnChangePlayerEventSO : ScriptableObject
{
    public Action<PlayerCharacter> OnChangeEnemy;

    public void RaiseEvent(PlayerCharacter newPlayer)
    {
        OnChangeEnemy?.Invoke(newPlayer);
    }

    public void Register(Action<PlayerCharacter> listener)
    {
        OnChangeEnemy += listener;
    }

    public void Unregister(Action<PlayerCharacter> listener)
    {
        OnChangeEnemy -= listener;
    }
}
