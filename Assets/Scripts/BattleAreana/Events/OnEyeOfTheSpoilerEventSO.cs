using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "On Eye Of The Spoiler Event", menuName = "Events/On Eye Of The Spoiler Event")]
public class OnEyeOfTheSpoilerEventSO : ScriptableObject
{
    public UnityAction OnEyeOfTheSpoilerEvent;

    public void RaiseEvent()
    {
        OnEyeOfTheSpoilerEvent?.Invoke();
    }

    public void Register(UnityAction listener)
    {
        OnEyeOfTheSpoilerEvent += listener;
    }

    public void Unregister(UnityAction listener)
    {
        OnEyeOfTheSpoilerEvent -= listener;
    }
}
