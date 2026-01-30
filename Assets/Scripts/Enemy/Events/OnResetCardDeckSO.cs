using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "On Reset Card Deck", menuName = "Events/Enemy/On Reset Card Deck")]
public class OnResetCardDeckSO : ScriptableObject
{
    public UnityEvent OnResetCardDeck;

    public void OnRaise()
    {
        OnResetCardDeck?.Invoke();
    }

    public void Register(UnityAction listener)
    {
        OnResetCardDeck.AddListener(listener);
    }

    public void Unregister(UnityAction listener)
    {
        OnResetCardDeck.RemoveListener(listener);
    }
}
