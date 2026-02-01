using UnityEngine;

public enum EnemyType
{
    Goon,
    Boss
}

public enum EnemyStatus
{
    Normal,
    Strong
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public EnemyType enemyType;
    public EnemyStatus enemyStatus;
    public float baseDamage;
    public float maxHealth;
    public int hiddenCard;
    public bool isElimination;
}
