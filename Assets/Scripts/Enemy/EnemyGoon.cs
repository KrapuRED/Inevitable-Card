using UnityEngine;

public class EnemyGoon : Enemy
{
    [SerializeField] private CurrentEnemyHandler currentEnemyHandler;
    [SerializeField] private TargetType type;

    [Header("Events")]
    public OnTakeDamageEventSO onTakeDamage;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Start()
    {
        if (enemyData != null)
        {
            _spriteRenderer.color = Color.red;
            nameCharacter = enemyData.enemyName;
            maxHealtPoint = enemyData.maxHealth;
            baseDamage = enemyData.baseDamage;
        }

        SetCurrentEnemy();
    }

    public void SetCurrentEnemy()
    {
        if (currentEnemyHandler == null)
        {
            currentEnemyHandler = GameObject.FindGameObjectWithTag("EventEnemyHandler")
                                            .GetComponent<CurrentEnemyHandler>();
        }

        currentEnemyHandler.SetCurrentEnemy(this);
    }

    public void OnTakeDamage(TargetType targetType, int damageValue)
    {
        if (targetType == type)
        {
            TakeDamage(damageValue);
        }
    }
}
