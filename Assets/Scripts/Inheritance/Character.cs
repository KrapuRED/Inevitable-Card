using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [Header("Detail Character")]
    public string nameCharacter;
    public float  maxHealtPoint;
    public float  baseDamage;


    public float healtPoints { get; set; }

    public virtual void DealDamage()
    {

    }

    public void TakeHealing(int healValue)
    {
        healtPoints += (float)healValue;
        healtPoints = Mathf.Clamp(healtPoints, 0, maxHealtPoint);

        OnHealthChanged();

        if (healtPoints > 100)
        {
            Debug.LogWarning("healtpoint is above 100!");
        }
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log($"{this.name} is TakeDamage called with: {damage}");
        healtPoints -= damage;
        healtPoints = Mathf.Clamp(healtPoints, 0, maxHealtPoint);

        OnHealthChanged();

        if (healtPoints <= 0)
        {
            OnDeath();
        }
        //Debug.Log($"HP : {healtPoints}");
    }

    protected virtual void OnHealthChanged()
    {

    }

    public virtual void OnDeath()
    {
    }
}
