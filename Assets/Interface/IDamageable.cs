using UnityEngine;

public interface IDamageable
{
    float healtPoints { get; set; }
    public void TakeDamage(float damage);
}
