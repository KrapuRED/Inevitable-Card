using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Register Card Event", menuName = "Global Events/Register Card Event")]
public class RegisterCardEventSO : ScriptableObject
{
    public UnityAction<CardDeck> OnRegisterCard;

    public void OnRaise(CardDeck card)
    {
        OnRegisterCard?.Invoke(card);
    }

    public void Register(UnityAction<CardDeck> listener)
    {
        OnRegisterCard += listener;
    }

    public void Unregister(UnityAction<CardDeck> listener)
    {
        OnRegisterCard -= listener;
    }
}
