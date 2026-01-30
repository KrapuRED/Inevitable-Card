using System;
using UnityEngine;

[CreateAssetMenu(fileName = "On Change Enemy Event", menuName = "Events/On Change Enemy Event")]
public class OnChangeEnemyEventSO : ScriptableObject
{
    public Action<Enemy> OnChangeEnemy;

    public void RaiseEvent(Enemy newEnemy)
    {
        OnChangeEnemy?.Invoke(newEnemy);
    }   

    public void Register(Action<Enemy> listener)
    {
        OnChangeEnemy += listener;
    }

    public void Unregister(Action<Enemy> listener)
    {
        OnChangeEnemy -= listener;
    }
}
