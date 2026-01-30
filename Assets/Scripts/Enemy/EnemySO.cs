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
    public int baseDamage;
    public int maxHealth;
    public int hiddenCard;
    public bool isElimination;
}
