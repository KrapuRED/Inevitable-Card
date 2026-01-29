using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "On Set Enemy Card Deck Event", menuName = "Events/Enemy/On Set Enemy Card Deck Event")]
public class OnSetEnemyCardDeckEventSO : ScriptableObject
{
    public Action<int, CardInstance[]> OnSetEnemyDeck;

    public void OnRaiseEvent(int hidenCards, CardInstance[] cards)
    {
        OnSetEnemyDeck?.Invoke(hidenCards, cards);
    }

    public void Register(Action<int, CardInstance[]> Listener)
    {
        OnSetEnemyDeck += Listener;
    }

    public void Unregister(Action<int, CardInstance[]> Listener)
    {
        OnSetEnemyDeck -= Listener;
    }
}
