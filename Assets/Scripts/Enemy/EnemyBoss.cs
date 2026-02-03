using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    [SerializeField] private CurrentEnemyHandler _currentEnemyHandler;
    [SerializeField] private EnemyPickCard _currentEnemyPickCard;
    [SerializeField] private TargetType _enemyType;
    private bool isDead;

    [Header("Boss Deck")]
    [SerializeField] private List<CardSO> bossCard;

    [Header("Events")]
    public OnTakeDamageEventSO onTakeDamageEvent;
    public OnTakingHealEventSO onTakingHealEvent;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Start()
    {
        if (enemyData != null)
        {
            
            nameCharacter = enemyData.enemyName;
            maxHealtPoint = enemyData.maxHealth;
            healtPoints = enemyData.maxHealth;
            baseDamage = enemyData.baseDamage;
        }


        HUDManager.instance.UpdateEnemyHealth(healtPoints, maxHealtPoint);
        HUDManager.instance.UpdateEnemyBaseDamage(baseDamage);
        SetCurrentEnemy();
    }

    protected override void OnHealthChanged()
    {
        Debug.Log("Get called OnHealthChanged");
        HUDManager.instance.UpdateEnemyHealth(healtPoints, maxHealtPoint);
    }

    public void SetCurrentEnemy()
    {
        if (_currentEnemyHandler == null)
        {
            _currentEnemyHandler = GetComponentInChildren<CurrentEnemyHandler>();

        }

        _currentEnemyHandler.SetCurrentEnemy(this);
    }

    public void OnTakeDamage(TargetType targetType, int damageValue)
    {
        Debug.Log($"{gameObject.name} is take damage with value {damageValue}");
        if (targetType == _enemyType)
        {
            //Debug.Log($"{this.name} with the target type of {targetType} is take damage {damageValue}");
            TakeDamage(damageValue);
        }
    }

    private void OnTakingHealing(TargetType targetType, int damageValue)
    {
        if (targetType == _enemyType)
        {
            //Debug.Log($"{this.name} with the target type of {targetType} is take damage {damageValue}");
            TakeHealing(damageValue);
        }
    }

    public override void OnDeath()
    {
        //Go Second Phase
        EnterSecondPhase();
        
    }

    private void EnterSecondPhase()
    {
       Debug.Log("We go Second Phase");
        if (isDead) return;

        isDead = true; // prevents looping

        // 1. Set boss deck pool
        _currentEnemyPickCard.SetCardDeckPool(bossCard);

        // 2. Reset HP
        healtPoints = maxHealtPoint;
        HUDManager.instance.UpdateEnemyHealth(healtPoints, maxHealtPoint);

        // 3. Tell BattleManager to rebuild enemy deck (SEQUENTIAL + REVEALED)
        BattleManager.instance.RefreshEnemyDeckPhase();
    }

    private void OnEnable()
    {
        onTakeDamageEvent.Register(OnTakeDamage);
        onTakingHealEvent.Register(OnTakingHealing);
    }

    private void OnDisable()
    {
        onTakeDamageEvent.Unregister(OnTakeDamage);
        onTakingHealEvent.Unregister(OnTakingHealing);
    }
}
