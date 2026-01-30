using UnityEngine;

public class CurrentPlayerHandler : MonoBehaviour
{
    [Header("Events")]
    public OnChangePlayerEventSO OnChangePlayerEvent;

    public void SetCurrentPlayer(PlayerCharacter player)
    {
       OnChangePlayerEvent.RaiseEvent(player);
    }
}
