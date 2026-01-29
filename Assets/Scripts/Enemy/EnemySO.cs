using UnityEngine;

public enum EnemyType
{
    Prototype,
    Boss
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public EnemyType enemyType;
    public int damage;
    public int health;
    public int hiddenCard;
    public bool isElimination;
}
