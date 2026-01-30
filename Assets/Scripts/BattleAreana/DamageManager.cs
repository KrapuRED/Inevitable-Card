using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance;

    [Header("Events")]
    public OnTakeDamageEventSO onTakeDamage;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void DealDamageToTarget(TargetType target, int damageValue)
    {
        onTakeDamage.OnRaise?.Invoke(target, damageValue);
    }

    public void HealToTarget(string target, int damageValue)
    {
        Debug.Log($"Healing for {target} with the value of {damageValue}");
    }
}
