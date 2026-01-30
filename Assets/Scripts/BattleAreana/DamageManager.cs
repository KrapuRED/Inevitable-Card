using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void DealDamageToTarget(string sender, string target, int damageValue)
    {
        Debug.Log($"{sender} dealing damage to {target} with the value of {damageValue}");

    }

    public void HealToTarget(string target, int damageValue)
    {
        Debug.Log($"Healing for {target} with the value of {damageValue}");
    }
}
