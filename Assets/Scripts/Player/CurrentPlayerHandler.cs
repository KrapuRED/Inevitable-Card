using UnityEngine;

public class CurrentPlayerHandler : MonoBehaviour
{
    [Header("Events")]
    public OnChangePlayerEventSO OnChangePlayerEvent;

    public void SetCurrentPlayer(Player player)
    {
       OnChangePlayerEvent.RaiseEvent(player);
    }
}
