using UnityEngine;

public enum EnemyType
{
    Goon,
    Boss
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public EnemyType enemyType;
    public float baseDamage;
    public float maxHealth;
    public int hiddenCard;
    public bool isElimination;
}
