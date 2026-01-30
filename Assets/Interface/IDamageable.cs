using UnityEngine;

public interface IDamageable
{
    int healtPoints { get; set; }
    public void TakeDamage(int damage);
}
