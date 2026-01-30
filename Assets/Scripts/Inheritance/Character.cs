using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Detail Character")]
    public string nameCharacter;
    public int  maxHealtPoint;
    public int  baseDamage;

    public float healtPoints { get; set; }

    public virtual void DealDamage()
    {

    }

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage called with: " + damage);
        healtPoints -= damage;
        if (healtPoints <= 0)
        {
            Debug.Log($"{nameCharacter} is dead!");
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
    }
}
