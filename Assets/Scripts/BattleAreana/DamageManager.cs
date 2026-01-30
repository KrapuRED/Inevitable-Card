using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance;

    [Header("Events")]
    public OnTakeDamageEventSO onTakeDamage;
    public OnTakingHealEventSO onTakingHeal;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void DealDamageToTarget(TargetType target, int damageValue)
    {
        onTakeDamage.OnRaise.Invoke(target, damageValue);
    }

    public void HealToTarget(TargetType target, int healValue)
    {
        onTakingHeal.OnRaise(target, healValue);
    }
}
