using UnityEngine;

public class Enemy : Character
{
    public EnemySO enemyData;
    [SerializeField] protected EnemyAnimation _enemyAnimation;
    public EnemyAnimation enemyAnimation => _enemyAnimation;
}
