using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [Header("Detail Character")]
    public string nameCharacter;
    public float  maxHealtPoint;
    public float  baseDamage;

    [Header("reference")]
    public HUDBorderCard hudUI;

    public float healtPoints { get; set; }

    public virtual void DealDamage()
    {

    }

    public void TakeDamage(float damage)
    {
        //Debug.Log($"{this.name} is TakeDamage called with: {damage}");
        healtPoints -= damage;
        if (healtPoints <= 0)
        {
            //Debug.Log($"{nameCharacter} is dead!");
            OnDeath();
        }
        //Debug.Log($"HP : {healtPoints}");
    }

    public virtual void OnDeath()
    {
    }
}
