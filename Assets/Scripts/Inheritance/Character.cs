using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [Header("Detail Character")]
    public string nameCharacter;
    public int  maxHealtPoint;
    public int  baseDamage;

    public int healtPoints { get; set; }

    public virtual void DealDamage()
    {

    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{this.name} is TakeDamage called with: {damage}");
        healtPoints -= damage;
        if (healtPoints <= 0)
        {
            Debug.Log($"{nameCharacter} is dead!");
            OnDeath();
        }
        Debug.Log($"HP : {healtPoints}");
    }

    public virtual void OnDeath()
    {
    }
}
